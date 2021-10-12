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
            string opportunityId = Console.ReadLine();

            Console.WriteLine(CalcularDesconto.CalcularDescontoPorIdOportunidade(opportunityId));

            Console.ReadKey();
        }
    }
}
