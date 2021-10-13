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
            string accountLevel = "";
            string secondLevel = "";
            string discountLevel = "";

            foreach (Entity opportunityCRM in opportunitiesCRM.Entities)
            {
                string name = opportunityCRM.Contains("name") ? opportunityCRM["name"].ToString() : "Oportunidade não possui um tópico cadastrado...";

                string accountId = opportunityCRM.Contains("conta.accountid") ? ((AliasedValue)opportunityCRM["conta.accountid"]).Value.ToString() : "Oportunidade não possui um ID de conta...";
                string accountName = opportunityCRM.Contains("conta.name") ? ((AliasedValue)opportunityCRM["conta.name"]).Value.ToString() : "Oportunidade não possui uma conta relacionada...";
                Console.WriteLine($"\nAccountID = {accountId}\n");

                EntityCollection clientLevelsCRM = opportunity.RetrieveClientLevelByAccount(new Guid(accountId));

                foreach (Entity clientLevelCRM in clientLevelsCRM.Entities)
                {
                    accountLevel = clientLevelCRM.Contains("niveldocliente.lcd_name") ? ((AliasedValue)clientLevelCRM["niveldocliente.lcd_name"]).Value.ToString() : "Cliente não possui um nível definido...";
                    secondLevel = clientLevelCRM.Contains("niveldocliente.lcd_nivel") ? ((AliasedValue)clientLevelCRM["niveldocliente.lcd_nivel"]).Value.ToString() : "Cliente não possui um nível definido...";
                    discountLevel = clientLevelCRM.Contains("niveldocliente.lcd_desconto") ? ((AliasedValue)clientLevelCRM["niveldocliente.lcd_desconto"]).Value.ToString() : "Cliente não possui um desconto definido...";
                }

                Console.WriteLine($"Oportunidade: {name}");
                Console.WriteLine($"Cliente: {accountName}");
                Console.WriteLine($"Nome do Nível do Cliente (accountLevel): {accountLevel}");
                Console.WriteLine($"Nível do Cliente (secondLevel): {secondLevel}");
                Console.WriteLine($"Desconto do Cliente (discountLevel): {discountLevel}");
            }
        }
    }
}
