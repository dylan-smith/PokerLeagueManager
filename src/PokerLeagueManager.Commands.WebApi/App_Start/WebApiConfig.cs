﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PokerLeagueManager.Commands.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "QueryApi",
                routeTemplate: "api/{controller}/{action}");
        }
    }
}
