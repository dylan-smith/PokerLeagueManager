using System;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;

namespace PokerLeagueManager.UI.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AIHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            // If customError is Off, then AI HTTPModule will report the exception
            if (filterContext?.HttpContext?.IsCustomErrorEnabled == true && filterContext?.Exception != null)
            {
                var ai = new TelemetryClient();
                ai.TrackException(filterContext.Exception);
            }

            base.OnException(filterContext);
        }
    }
}