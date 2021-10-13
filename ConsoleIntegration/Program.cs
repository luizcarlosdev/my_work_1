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
            // string opportunityId = "d9c86249-f02a-ec11-b6e6-00224837afc9"; //TESTE
            string opportunityId = Console.ReadLine();

            RetrieveDiscountByOpportunity(opportunity, opportunityId);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void RetrieveDiscountByOpportunity(Opportunity opportunity, string opportunityId)
        {
            EntityCollection opportunitiesCRM = opportunity.RetrieveClientByOpportunity(new Guid(opportunityId));
            string accountLevel = string.Empty;
            string secondLevel = string.Empty;
            string discountLevel = string.Empty;

            foreach (Entity opportunityCRM in opportunitiesCRM.Entities)
            {
                string name = opportunityCRM.Contains("name") ? opportunityCRM["name"].ToString() : "Oportunidade não possui um tópico cadastrado...";
                Money totalAmount = opportunityCRM.Contains("totallineitemamount") ? (Money)opportunityCRM["totallineitemamount"] : null;
                
                string accountId = opportunityCRM.Contains("conta.accountid") ? ((AliasedValue)opportunityCRM["conta.accountid"]).Value.ToString() : "Oportunidade não possui um ID de conta...";
                string accountName = opportunityCRM.Contains("conta.name") ? ((AliasedValue)opportunityCRM["conta.name"]).Value.ToString() : "Oportunidade não possui uma conta relacionada...";

                EntityCollection clientLevelsCRM = opportunity.RetrieveClientLevelByAccount(new Guid(accountId));

                foreach (Entity clientLevelCRM in clientLevelsCRM.Entities)
                {
                    accountLevel = clientLevelCRM.Contains("niveldocliente.lcd_name") ? ((AliasedValue)clientLevelCRM["niveldocliente.lcd_name"]).Value.ToString() : "Cliente não possui um nível definido...";
                    secondLevel = clientLevelCRM.Contains("niveldocliente.lcd_nivel") ? ((AliasedValue)clientLevelCRM["niveldocliente.lcd_nivel"]).Value.ToString() : "Cliente não possui um nível definido...";
                    discountLevel = clientLevelCRM.Contains("niveldocliente.lcd_desconto") ? ((AliasedValue)clientLevelCRM["niveldocliente.lcd_desconto"]).Value.ToString() : "Cliente não possui um desconto definido...";

                    Console.WriteLine($"\nDesconto do Cliente: " + String.Format("{0:0.00}", double.Parse(discountLevel)) + "%");
                    Console.WriteLine("Valor total da oportunidade: " + String.Format("{0:0.00}", totalAmount.Value));
                    Console.WriteLine("Valor total do desconto: " + String.Format("{0:0.00}", ((double)totalAmount.Value * double.Parse(discountLevel) / 100)));
                    Console.WriteLine("Valor total da oportunidade com desconto: " + String.Format("{0:0.00}", ((double)totalAmount.Value - ((double)totalAmount.Value * (double.Parse(discountLevel) / 100)))));

                    Console.WriteLine("\nVocê deseja atualizar essa oportunidade? Y/N");
                    string choice = Console.ReadLine().ToUpper();

                    if (choice == "Y")
                    {
                        double discountValue = (double)totalAmount.Value * (double.Parse(discountLevel) / 100);
                        opportunity.UpdateDiscountByOpportunity(new Guid(opportunityId), discountValue);
                    }
                    else
                    {
                        Console.WriteLine("Operação finalizada. Agradecemos sua interação!");
                    }
                }
            }
        }
    }
}
