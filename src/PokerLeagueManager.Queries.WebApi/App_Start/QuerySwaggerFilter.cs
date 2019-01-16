using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Http.Description;
using PokerLeagueManager.Common.Infrastructure;
using Swashbuckle.Swagger;

namespace PokerLeagueManager.Queries.WebApi
{
    public class QuerySwaggerFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.produces = new List<string>();
            swaggerDoc.produces.Add("application/json");

            swaggerDoc.consumes = new List<string>();
            swaggerDoc.consumes.Add("application/json");

            swaggerDoc.tags = new List<Tag>();
            var queryTag = new Tag();
            queryTag.name = "Queries";
            queryTag.description = "All the queries your heart desires!";
            swaggerDoc.tags.Add(queryTag);

            var queryTypes = new List<Type>();
            queryTypes.AddRange(typeof(BaseQuery).Assembly.GetTypes());
            queryTypes = queryTypes.Where(t => t.IsClass && t.BaseType == typeof(BaseQuery)).ToList();

            swaggerDoc.paths = new Dictionary<string, PathItem>();

            foreach (var query in queryTypes)
            {
                var pathItem = GeneratePathItem(query);
                var queryAction = query.Name.Substring(0, query.Name.Length - "Query".Length);
                swaggerDoc.paths.Add($"/{queryAction}", pathItem);
            }
        }

        private PathItem GeneratePathItem(Type query)
        {
            var pathItem = new PathItem();

            var postOperation = new Operation();
            postOperation.tags = new List<string>();
            postOperation.tags.Add("Queries");

            postOperation.operationId = query.Name;

            var queryDesc = query.GetCustomAttribute<DescriptionAttribute>(false);
            var querySummary = query.GetCustomAttribute<SummaryAttribute>(false);

            if (queryDesc != null)
            {
                postOperation.description = queryDesc.Description;
            }

            if (querySummary != null)
            {
                postOperation.summary = querySummary.Summary;
            }

            var queryParam = GenerateParameter(query);
            if (queryParam.schema.properties.Count > 0)
            {
                postOperation.parameters = new List<Parameter>();
                postOperation.parameters.Add(GenerateParameter(query));
            }

            postOperation.responses = new Dictionary<string, Response>();
            var queryResponse = GenerateResponse(query);
            queryResponse.description = "Success";
            postOperation.responses.Add("200", queryResponse);

            pathItem.post = postOperation;

            return pathItem;
        }

        private Response GenerateResponse(Type query)
        {
            var queryReturnType = GetQueryReturnType(query);

            if (typeof(IDataTransferObject).IsAssignableFrom(queryReturnType))
            {
                return GenerateDtoResponse(queryReturnType);
            }

            if (typeof(IEnumerable<IDataTransferObject>).IsAssignableFrom(queryReturnType))
            {
                return GenerateListResponse(queryReturnType);
            }

            if (typeof(int) == queryReturnType)
            {
                return GenerateIntResponse();
            }

            throw new InvalidOperationException("Unexpected return type from query");
        }

        private Response GenerateIntResponse()
        {
            var responseSchema = new Schema();
            responseSchema.type = "integer";

            var response = new Response();
            response.schema = responseSchema;

            return response;
        }

        private Response GenerateDtoResponse(Type queryReturnType)
        {
            var dtoSchema = new Schema();
            dtoSchema.type = "object";
            dtoSchema.properties = new Dictionary<string, Schema>();
            dtoSchema.title = queryReturnType.Name;

            foreach (var prop in queryReturnType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
            {
                dtoSchema.properties.Add(prop.Name, GenerateSchema(prop));
            }

            var response = new Response();
            response.schema = dtoSchema;

            return response;
        }

        private Response GenerateListResponse(Type queryReturnType)
        {
            var dtoType = queryReturnType.GenericTypeArguments[0];

            var dtoSchema = new Schema();
            dtoSchema.type = "object";
            dtoSchema.properties = new Dictionary<string, Schema>();
            dtoSchema.title = dtoType.Name;

            foreach (var prop in dtoType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
            {
                dtoSchema.properties.Add(prop.Name, GenerateSchema(prop));
            }

            var responseSchema = new Schema();
            responseSchema.type = "array";
            responseSchema.items = dtoSchema;
            responseSchema.title = $"Array of {dtoType.Name}";

            var response = new Response();
            response.schema = responseSchema;

            return response;
        }

        private Parameter GenerateParameter(Type query)
        {
            var queryProperties = query.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

            var querySchema = new Schema();
            querySchema.type = "object";
            querySchema.properties = new Dictionary<string, Schema>();
            querySchema.title = query.Name;

            foreach (var prop in queryProperties)
            {
                querySchema.properties.Add(prop.Name, GenerateSchema(prop));
            }

            var queryParam = new Parameter();
            queryParam.name = query.Name;
            queryParam.@in = "body";
            queryParam.description = "Arguments for the query";
            queryParam.schema = querySchema;

            return queryParam;
        }

        private Schema GenerateSchema(PropertyInfo prop)
        {
            var propDesc = prop.GetCustomAttribute<DescriptionAttribute>(false);

            var propSchema = new Schema();
            propSchema.type = GetSwaggerType(prop.PropertyType);
            propSchema.format = GetSwaggerFormat(prop.PropertyType);
            propSchema.description = propDesc?.Description;

            return propSchema;
        }

        private string GetSwaggerType(Type propertyType)
        {
            var mapping = new Dictionary<Type, string>()
            {
                { typeof(int), "integer" },
                { typeof(long), "integer" },
                { typeof(float), "number" },
                { typeof(double), "number" },
                { typeof(string), "string" },
                { typeof(DateTime), "string" },
                { typeof(Guid), "string" },
                { typeof(bool), "boolean" },
            };

            return mapping[propertyType];
        }

        private string GetSwaggerFormat(Type propertyType)
        {
            var mapping = new Dictionary<Type, string>()
            {
                { typeof(int), string.Empty },
                { typeof(long), string.Empty },
                { typeof(float), string.Empty },
                { typeof(double), string.Empty },
                { typeof(string), string.Empty },
                { typeof(DateTime), "date" },
                { typeof(Guid), "uuid" },
                { typeof(bool), string.Empty },
            };

            return mapping[propertyType];
        }

        private Type GetQueryReturnType(Type queryType)
        {
            var queryInterface = queryType.GetInterfaces().Single(i => i.IsGenericType && i.Name.StartsWith("IQuery"));
            return queryInterface.GenericTypeArguments[0];
        }
    }
}