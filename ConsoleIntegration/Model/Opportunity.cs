using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegration.Model
{
    public class Opportunity
    {
        public string TableName = "opportunity";

        public IOrganizationService Service { get; set; }

        public Opportunity(IOrganizationService service)
        {
            this.Service = service;
        }
    }
}
