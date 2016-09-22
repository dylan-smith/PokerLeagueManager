using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace PokerLeagueManager.UI.Web
{
    public partial class Startup
    {
        private static string _clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string _aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string _tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string _postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string _authority = _aadInstance + _tenantId;

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = _clientId,
                    Authority = _authority,
                    PostLogoutRedirectUri = _postLogoutRedirectUri
                });
        }
    }
}
