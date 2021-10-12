using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace ConsoleIntegration
{
    public class ConnectionFactory
    {
        public static IOrganizationService GetCrmService()
        {
            string connectionString =
                "AuthType=OAuth;" +
                "Username=luizcarlosdev@torisoftwares.onmicrosoft.com;" +
                "Password=LZyLckq%mG7T;" +
                "Url=https://orgfa269e66.crm2.dynamics.com;" +
                "AppId=c62397f9-ef28-46b0-bf92-770523006057;" +
                "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}