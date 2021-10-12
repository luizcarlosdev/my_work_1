using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegration.Model
{
    public static class CalcularDesconto
    {
        public static string CalcularDescontoPorIdOportunidade(string OpportunityId)
        {
            return $"Calculando o desconto da oportunidade {OpportunityId}";
        }
    }
}
