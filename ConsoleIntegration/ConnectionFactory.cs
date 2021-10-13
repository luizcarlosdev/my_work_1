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
                "Url=https://org578bc400.crm2.dynamics.com/;" +
                "AppId=7a38e6d8-34d2-4fe2-8d3c-70066da06560;" +
                "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}