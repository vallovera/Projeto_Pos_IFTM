using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using MusicaSPA.Models;
using MusicaSPA.Providers;

namespace MusicaSPA
{
    public partial class Startup
    {
        // Habilitar o aplicativo a usar OAuthAuthorization. Então poderá proteger suas APIs da Web
        static Startup()
        {
            //PublicClientId = "web";

            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/Token"),
            //    AuthorizeEndpointPath = new PathString("/Account/Authorize"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //    AllowInsecureHttp = true
            //};
        }

       
        
    }
}
