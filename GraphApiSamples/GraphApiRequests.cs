using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace GraphApiSamples
{
    public class GraphApiRequests
    {
        private ActiveDirectoryClient client;
        private readonly ILogger logger;

        public GraphApiRequests(ILogger logger)
        {
            this.logger = logger;
        }

        public void Initialize()
        {
            try
            {
                client = AuthenticationHelper.GetActiveDirectoryClientAsUser();
            }
            catch (Exception e)
            {
                logger.WriteError($"Authentication failed because of the following reasons : {e.ExtractErrorMessage()}");
            }
        }

        public async Task CreateNewUserAsync()
        {
            var tenantDetail = await GetTenantDetails(ConfigSettings.Instance.TenantId);
            var newUser = new User();

            logger.WriteLine("Please enter first name for the user");
            var firstName = logger.ReadLine();
            logger.WriteLine("Please enter last name for the user");
            var lastName = logger.ReadLine();

            newUser.DisplayName = $"{firstName} {lastName}";
            newUser.UserPrincipalName = $"{firstName}.{lastName}@{tenantDetail.VerifiedDomains.First(x => x.@default.HasValue && x.@default.Value).Name}";
            newUser.AccountEnabled = true;
            newUser.MailNickname = firstName + lastName;
            newUser.PasswordProfile = new PasswordProfile
            {
                Password = "ChangeMe123!",
                ForceChangePasswordNextLogin = true
            };
            newUser.UsageLocation = "US";
            try
            {
                await client.Users.AddUserAsync(newUser);
                logger.WriteLine($"New User {newUser.DisplayName} was created");
            }
            catch (Exception e)
            {
                logger.WriteError($"Error creating new user {e.ExtractErrorMessage()}");
            }
        }

        private async Task<ITenantDetail> GetTenantDetails(string tenantId)
        {
            ITenantDetail tenant = null;

            try
            {
                var tenantsCollection = await client.TenantDetails
                         .Where(t => t.ObjectId.Equals(tenantId))
                         .ExecuteAsync();

                tenant = tenantsCollection.CurrentPage.FirstOrDefault();
            }
            catch (Exception e)
            {
                logger.WriteError($"Error getting tenant details {e.ExtractErrorMessage()}");
            }
            return tenant;   
        }
    }
}
