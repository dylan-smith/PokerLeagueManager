using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PokerLeagueManager.TypeScriptGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 6)
            {
                Console.WriteLine("Usage: PokerLeagueManager.TypeScriptGenerator.exe QuerySourcePath DtoSourcePath QueryClientPath DtoTypeScriptPath CommandSourcePath CommandClientPath");
                return;
            }

            var querySourcePath = args[0];
            var dtoSourcePath = args[1];
            var queryClientPath = args[2];
            var dtoTypeScriptPath = args[3];
            var commandSourcePath = args[4];
            var commandClientPath = args[5];

            var queries = ReadQueryFiles(querySourcePath);
            var dtos = ReadDtoFiles(dtoSourcePath);
            var commands = ReadCommandFiles(commandSourcePath);

            foreach (var dto in dtos)
            {
                var dtoTypeScript = GenerateDtoTypeScript(dto);
                WriteFile(dtoTypeScript, "I" + dto.Name + ".ts", dtoTypeScriptPath);
            }

            var queryTypeScript = GenerateQueryTypeScript(queries, dtos);
            WriteFile(queryTypeScript, "query.service.ts", queryClientPath);

            var commandTypeScript = GenerateCommandTypeScript(commands);
            WriteFile(commandTypeScript, "command.service.ts", commandClientPath);
        }

        private static void WriteFile(string fileContents, string fileName, string filePath)
        {
            var writePath = Path.Combine(filePath, fileName);

            if (File.Exists(writePath))
            {
                File.Delete(writePath);
            }

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            Console.WriteLine($"Writing File: {writePath}");
            File.WriteAllText(writePath, fileContents);
        }

        private static IEnumerable<Query> ReadQueryFiles(string querySourcePath)
        {
            var queryFiles = Directory.GetFiles(querySourcePath, "*Query.cs");
            var result = new List<Query>();

            foreach (var queryFile in queryFiles)
            {
                result.Add(ReadQueryFile(queryFile));
            }

            return result;
        }

        private static IEnumerable<Command> ReadCommandFiles(string commandSourcePath)
        {
            var commandFiles = Directory.GetFiles(commandSourcePath, "*Command.cs");
            var result = new List<Command>();

            foreach (var commandFile in commandFiles)
            {
                result.Add(ReadCommandFile(commandFile));
            }

            return result;
        }

        private static Query ReadQueryFile(string queryFile)
        {
            var result = new Query();

            result.Name = Path.GetFileNameWithoutExtension(queryFile);

            var source = File.ReadAllText(queryFile);
            var tree = CSharpSyntaxTree.ParseText(source);
            var root = tree.GetCompilationUnitRoot();
            var props = root.DescendantNodes().OfType<PropertyDeclarationSyntax>();

            foreach (var prop in props)
            {
                result.Properties.Add(new Prop() { Name = prop.Identifier.ToString(), Type = prop.Type.ToString() });
            }

            var queryInterfaceBaseType = root.DescendantNodes().OfType<SimpleBaseTypeSyntax>().Single(x => x.Type.ToString().StartsWith("IQuery"));
            var queryInterfaceText = queryInterfaceBaseType.Type.ToString();

            var queryReturn = queryInterfaceText.Substring("IQuery<".Length, queryInterfaceText.Length - ("IQuery<".Length + 1));

            if (queryReturn.StartsWith("IEnumerable<"))
            {
                result.ReturnType = QueryReturnType.Array;
                result.Returns = "I" + queryReturn.Substring("IEnumerable<".Length, queryReturn.Length - ("IEnumerable<".Length + 1));
                result.Returns += "[]";
            }
            else if (queryReturn.EndsWith("Dto"))
            {
                result.ReturnType = QueryReturnType.Dto;
                result.Returns = "I" + queryReturn;
            }
            else
            {
                result.ReturnType = QueryReturnType.Primitive;
                result.Returns = MapType(queryReturn);
            }

            return result;
        }

        private static Command ReadCommandFile(string commandFile)
        {
            var result = new Command();

            result.Name = Path.GetFileNameWithoutExtension(commandFile);

            var source = File.ReadAllText(commandFile);
            var tree = CSharpSyntaxTree.ParseText(source);
            var root = tree.GetCompilationUnitRoot();
            var props = root.DescendantNodes().OfType<PropertyDeclarationSyntax>();

            foreach (var prop in props)
            {
                result.Properties.Add(new Prop() { Name = prop.Identifier.ToString(), Type = prop.Type.ToString() });
            }

            return result;
        }

        private static string MapType(string type)
        {
            switch (type)
            {
                case "int":
                    return "number";
                case "string":
                    return "string";
                case "DateTime":
                    return "string";
                case "double":
                    return "number";
                case "Guid":
                    return "string";
                default:
                    throw new ArgumentException("Unrecognized type", nameof(type));
            }
        }

        private static IEnumerable<Dto> ReadDtoFiles(string dtoSourcePath)
        {
            var dtoFiles = Directory.GetFiles(dtoSourcePath, "*Dto.cs");
            var result = new List<Dto>();

            foreach (var dtoFile in dtoFiles)
            {
                result.Add(ReadDtoFile(dtoFile));
            }

            return result;
        }

        private static Dto ReadDtoFile(string dtoFile)
        {
            var result = new Dto();

            result.Name = Path.GetFileNameWithoutExtension(dtoFile);

            var source = File.ReadAllText(dtoFile);
            var tree = CSharpSyntaxTree.ParseText(source);
            var root = tree.GetCompilationUnitRoot();
            var props = root.DescendantNodes().OfType<PropertyDeclarationSyntax>();

            foreach (var prop in props)
            {
                result.Properties.Add(new Prop() { Name = prop.Identifier.ToString(), Type = prop.Type.ToString() });
            }

            return result;
        }

        private static string GenerateDtoTypeScript(Dto dto)
        {
            var dtoTypeScript = $"export interface I{dto.Name} {{\n";

            foreach (var prop in dto.Properties)
            {
                dtoTypeScript += $"  {prop.Name}: {prop.TypeScriptType()};\n";
            }

            dtoTypeScript += "}\n";

            return dtoTypeScript;
        }

        private static string GenerateQueryTypeScript(IEnumerable<Query> queries, IEnumerable<Dto> dtos)
        {
            var queryTypeScript = "import { Injectable, Inject } from '@angular/core';\n";
            queryTypeScript += "import { HttpClient, HttpErrorResponse } from '@angular/common/http';\n";
            queryTypeScript += "import { Observable } from 'rxjs/Observable';\n";
            queryTypeScript += "import 'rxjs/add/observable/throw';\n";
            queryTypeScript += "import 'rxjs/add/operator/catch';\n";
            queryTypeScript += "import 'rxjs/add/operator/do';\n";
            queryTypeScript += "import 'rxjs/add/operator/map';\n";
            queryTypeScript += "import { IPokerConfig } from './IPokerConfig';\n";
            queryTypeScript += "\n";

            foreach (var dto in dtos)
            {
                queryTypeScript += $"import {{ I{dto.Name} }} from './dtos/I{dto.Name}';\n";
            }

            queryTypeScript += "\n";

            foreach (var dto in dtos)
            {
                queryTypeScript += $"export {{ I{dto.Name} }} from './dtos/I{dto.Name}';\n";
            }

            queryTypeScript += "\n";
            queryTypeScript += "@Injectable()\n";
            queryTypeScript += "export class QueryService {\n";
            queryTypeScript += "  QUERY_URL: string;\n";
            queryTypeScript += "  constructor(private _http: HttpClient, @Inject('POKER_CONFIG') private POKER_CONFIG: IPokerConfig) {\n";
            queryTypeScript += "    this.QUERY_URL = POKER_CONFIG.queryServiceUrl;\n";
            queryTypeScript += "  }\n";
            queryTypeScript += "\n";

            foreach (var query in queries)
            {
                queryTypeScript += $"  {query.QueryAction()}(";

                foreach (var prop in query.Properties)
                {
                    queryTypeScript += $"{prop.Name}: {prop.TypeScriptType()}, ";
                }

                if (query.Properties.Count() > 0)
                {
                    queryTypeScript = queryTypeScript.Substring(0, queryTypeScript.Length - 2);
                }

                queryTypeScript += $"): Observable<{query.Returns}> {{\n";
                queryTypeScript += $"    return this._http.post<{query.Returns}>(this.QUERY_URL + '/{query.QueryAction()}', {{ ";

                foreach (var prop in query.Properties)
                {
                    queryTypeScript += $"{prop.Name}, ";
                }

                if (query.Properties.Count() > 0)
                {
                    queryTypeScript = queryTypeScript.Substring(0, queryTypeScript.Length - 2);
                }

                queryTypeScript += " });\n";
                queryTypeScript += "  }\n";
                queryTypeScript += "\n";
            }

            queryTypeScript += "}";

            return queryTypeScript;
        }

        private static string GenerateCommandTypeScript(IEnumerable<Command> commands)
        {
            var commandTypeScript = "import { Injectable, Inject } from '@angular/core';\n";
            commandTypeScript += "import { HttpClient, HttpErrorResponse } from '@angular/common/http';\n";
            commandTypeScript += "import { Observable } from 'rxjs/Observable';\n";
            commandTypeScript += "import 'rxjs/add/observable/throw';\n";
            commandTypeScript += "import 'rxjs/add/operator/catch';\n";
            commandTypeScript += "import 'rxjs/add/operator/do';\n";
            commandTypeScript += "import 'rxjs/add/operator/map';\n";
            commandTypeScript += "import { IPokerConfig } from './IPokerConfig';\n";
            commandTypeScript += "\n";

            commandTypeScript += "\n";
            commandTypeScript += "@Injectable()\n";
            commandTypeScript += "export class CommandService {\n";
            commandTypeScript += "  COMMAND_URL: string;\n";
            commandTypeScript += "  constructor(private _http: HttpClient, @Inject('POKER_CONFIG') private POKER_CONFIG: IPokerConfig) {\n";
            commandTypeScript += "    this.COMMAND_URL = POKER_CONFIG.commandServiceUrl;\n";
            commandTypeScript += "  }\n";
            commandTypeScript += "\n";

            foreach (var command in commands)
            {
                commandTypeScript += $"  {command.CommandAction()}(";

                foreach (var prop in command.Properties)
                {
                    commandTypeScript += $"{prop.Name}: {prop.TypeScriptType()}, ";
                }

                if (command.Properties.Count() > 0)
                {
                    commandTypeScript = commandTypeScript.Substring(0, commandTypeScript.Length - 2);
                }

                commandTypeScript += $"): Observable<void> {{\n";
                commandTypeScript += $"    return this._http.post<void>(this.COMMAND_URL + '/{command.CommandAction()}', {{ ";

                foreach (var prop in command.Properties)
                {
                    commandTypeScript += $"{prop.Name}, ";
                }

                if (command.Properties.Count() > 0)
                {
                    commandTypeScript = commandTypeScript.Substring(0, commandTypeScript.Length - 2);
                }

                commandTypeScript += " });\n";
                commandTypeScript += "  }\n";
                commandTypeScript += "\n";
            }

            commandTypeScript += "}";

            return commandTypeScript;
        }
    }
}
