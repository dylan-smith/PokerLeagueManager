using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Infrastructure;
using Unity;

namespace PokerLeagueManager.Commands.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommandController : ApiController, ICommandService
    {
        public HttpResponseMessage Post(string commandName, [FromBody]JToken jsonbody)
        {
            var commandType = GetCommandType(commandName);

            var command = (ICommand)Activator.CreateInstance(commandType);

            if (jsonbody != null)
            {
                command = (ICommand)JsonConvert.DeserializeObject(jsonbody.ToString(), commandType);
            }

            var ai = new TelemetryClient();
            ai.TrackEvent(commandType.Name, command.GetPropertiesDictionary());

            ExecuteCommand(command);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public void ExecuteCommand(ICommand command)
        {
            Resolver.Container.RegisterInstance<HttpContextWrapper>((HttpContextWrapper)Request.Properties["MS_HttpContext"]);

            var commandHandlerFactory = Resolver.Container.Resolve<ICommandHandlerFactory>();
            var commandFactory = Resolver.Container.Resolve<ICommandFactory>();
            commandHandlerFactory.ExecuteCommand(commandFactory.Create(command));
        }

        private Type GetCommandType(string commandName)
        {
            List<Type> assemblyTypes = new List<Type>();

            assemblyTypes.AddRange(typeof(BaseCommand).Assembly.GetTypes());

            return assemblyTypes.Single(t => t.IsClass && t.Name.ToLower() == $"{commandName}Command".ToLower());
        }
    }
}
