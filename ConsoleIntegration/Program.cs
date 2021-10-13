using System;
using ConsoleIntegration.Model;
using Microsoft.Xrm.Sdk;

namespace ConsoleIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService service = ConnectionFactory.GetCrmService();

            Opportunity opportunity = new Opportunity(service);
            
            Console.WriteLine("Qual oportunidade você deseja aplicar o desconto?");
            string opportunityId = "d9c86249-f02a-ec11-b6e6-00224837afc9"; //Console.ReadLine();

            RetrieveDiscountByOpportunity(opportunity, opportunityId);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void RetrieveDiscountByOpportunity(Opportunity opportunity, string opportunityId)
        {
            EntityCollection opportunitiesCRM = opportunity.RetrieveClientByOpportunity(new Guid(opportunityId));

            foreach(Entity opportunityCRM in opportunitiesCRM.Entities)
            {
                string name = opportunityCRM.Contains("name") ? opportunityCRM["name"].ToString() : "Oportunidade não possui um tópico cadastrado...";
                string accountName = opportunityCRM.Contains("conta.name") ? ((AliasedValue)opportunityCRM["conta.name"]).Value.ToString() : "Oportunidade não possui uma conta relacionada...";
                
                Console.WriteLine($"Oportunidade: {name}");
                Console.WriteLine($"Cliente: {accountName}");
            }
        }
    }
}
