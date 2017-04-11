using System;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace GraphApiSamples
{
    public static class AuthenticationHelper
    {
        private const string ResourceUrl = "https://graph.windows.net";
        private const string LoginUrl = "https://login.microsoftonline.com/";

        private static string token;

        public static ActiveDirectoryClient GetActiveDirectoryClientAsUser()
        {
            var serviceUri = new Uri(ResourceUrl);
            var serviceRoot = new Uri(serviceUri, ConfigSettings.Instance.TenantId);
            var client = new ActiveDirectoryClient(serviceRoot, async () => await AcquireTokenAsyncForUser());
            return client;
        }

        private static async Task<string> AcquireTokenAsyncForUser()
        {
            if (string.IsNullOrEmpty(token))
            {
                var redirectUri = new Uri("https://localhost");
                var context = new AuthenticationContext($"{LoginUrl}common", false);
                var authResult = await context.AcquireTokenAsync(ResourceUrl,
                    ConfigSettings.Instance.ClientId, redirectUri, new PlatformParameters(PromptBehavior.RefreshSession));

                token = authResult.AccessToken;
            }
            return token;
        }
    }
}
