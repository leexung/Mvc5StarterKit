using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Mvc5StarterKit.Models;

namespace Mvc5StarterKit.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof (ApplicationOAuthProvider));

        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var data = await context.Request.ReadFormAsync();
            string tenant = data["tenant"];

            var user = await userManager.FindTenantUserAsync(tenant, context.UserName, context.Password);
            if (user == null)
            {
                //Do not do this in your real application
                Logger.ErrorFormat("The user name [{0}] or tenant name [{1}] is incorrect.", context.UserName, tenant);
                context.SetError("invalid_grant", "The user name or tenant name is incorrect.");
                return;
            }

            bool isValidPassword = await userManager.CheckPasswordAsync(user, context.Password);

            if (isValidPassword == false)
            {
                //Do not do this in your real application
                Logger.ErrorFormat("The user name [{0}] or password [{1}] is incorrect.", context.UserName, context.Password);
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName, tenant);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, string tenant)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
            };
            if (!string.IsNullOrWhiteSpace(tenant))
                data.Add("tenant", tenant);
            return new AuthenticationProperties(data);
        }
    }
}