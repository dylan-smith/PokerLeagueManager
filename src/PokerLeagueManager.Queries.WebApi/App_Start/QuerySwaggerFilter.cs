using System.Collections.Generic;
using System.Web.Http.Description;
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

            var pathItem = new PathItem();
            var postOperation = new Operation();
            postOperation.tags = new List<string>();
            postOperation.tags.Add("Queries");
            postOperation.operationId = "GetGamesList";
            postOperation.summary = "Gets a list of all games for display in a list";

            var skipSchema = new Schema();
            skipSchema.type = "integer";
            skipSchema.description = "Used in combination with Take for paging the results.  Skip: 20, Take: 10 will get items 21-30";
            skipSchema.@default = 0;

            var takeSchema = new Schema();
            takeSchema.type = "integer";
            takeSchema.description = "Used in combination with Skip for paging the results.  Skip: 20, Take: 10 will get items 21-30";

            var skipParam = new Parameter();
            skipParam.name = "Skip";
            skipParam.description = "Used in combination with Take for paging the results.  Skip: 20, Take: 10 will get items 21-30";
            skipParam.@in = "body";
            skipParam.type = "integer";
            skipParam.@default = 0;
            skipParam.required = false;

            var takeParam = new Parameter();
            takeParam.name = "Take";
            takeParam.description = "Used in combination with Skip for paging the results.  Skip: 20, Take: 10 will get items 21-30";
            takeParam.@in = "body";
            takeParam.type = "integer";
            takeParam.required = false;

            var querySchema = new Schema();
            querySchema.type = "object";
            querySchema.description = "Optional parameters for this query";
            querySchema.properties = new Dictionary<string, Schema>();
            querySchema.properties.Add("Skip", skipSchema);
            querySchema.properties.Add("Take", takeSchema);

            var queryParam = new Parameter();
            queryParam.name = "GetGamesListQuery";
            queryParam.@in = "body";
            queryParam.description = "Optional parameters for this query";
            queryParam.schema = querySchema;
            queryParam.required = false;

            postOperation.parameters = new List<Parameter>();
            postOperation.parameters.Add(queryParam);

            var gameIdProp = new Schema();
            gameIdProp.type = "string";
            gameIdProp.format = "uuid";
            gameIdProp.description = "GUID used to uniquely identify the game";

            var gameDateProp = new Schema();
            gameDateProp.type = "string";
            gameDateProp.format = "date";
            gameDateProp.description = "The date the game was played";

            var winnerProp = new Schema();
            winnerProp.type = "string";
            winnerProp.description = "The player who won the game";

            var winningsProp = new Schema();
            winningsProp.type = "integer";
            winningsProp.description = "The total amount in dollars that the winner received";

            var dtoSchema = new Schema();
            dtoSchema.type = "object";
            dtoSchema.properties = new Dictionary<string, Schema>();
            dtoSchema.properties.Add("GameId", gameIdProp);
            dtoSchema.properties.Add("GameDate", gameDateProp);
            dtoSchema.properties.Add("Winner", winnerProp);
            dtoSchema.properties.Add("Winnings", winningsProp);

            var responseSchema = new Schema();
            responseSchema.type = "array";
            responseSchema.items = dtoSchema;

            var response = new Response();
            response.description = "List of games";
            response.schema = responseSchema;

            postOperation.responses = new Dictionary<string, Response>();
            postOperation.responses.Add("200", response);

            pathItem.post = postOperation;

            swaggerDoc.paths = new Dictionary<string, PathItem>();
            swaggerDoc.paths.Add("/GetGamesList", pathItem);
        }
    }
}