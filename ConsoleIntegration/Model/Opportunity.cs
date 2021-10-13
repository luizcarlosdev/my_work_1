using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegration.Model
{
    public class Opportunity
    {
        public IOrganizationService Service { get; set; }
        
        public string TableName = "opportunity";
                

        public Opportunity(IOrganizationService service)
        {
            this.Service = service;
        }

        public EntityCollection RetrieveClientByOpportunity(Guid opportunityId)
        {
            QueryExpression queryOpportunities = new QueryExpression(this.TableName);
            queryOpportunities.ColumnSet.AddColumns("name","parentaccountid", "totallineitemamount") ;
            queryOpportunities.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, opportunityId);

            queryOpportunities.AddLink("account", "parentaccountid", "accountid", JoinOperator.Inner);

            queryOpportunities.LinkEntities.FirstOrDefault().Columns.AddColumns("name", "accountid");
            queryOpportunities.LinkEntities.FirstOrDefault().EntityAlias = "conta";

            return this.Service.RetrieveMultiple(queryOpportunities);
        }

        public EntityCollection RetrieveClientLevelByAccount(Guid accountId)
        {
            QueryExpression queryClientLevel = new QueryExpression("account");

            queryClientLevel.ColumnSet.AddColumns("name");
            queryClientLevel.Criteria.AddCondition("accountid", ConditionOperator.Equal, accountId);

            queryClientLevel.AddLink("lcd_niveldocliente", "lcd_niveldocliente", "lcd_niveldoclienteid", JoinOperator.Inner);

            queryClientLevel.LinkEntities.FirstOrDefault().Columns.AddColumns("lcd_name", "lcd_nivel", "lcd_desconto");
            queryClientLevel.LinkEntities.FirstOrDefault().EntityAlias = "niveldocliente";

            return this.Service.RetrieveMultiple(queryClientLevel);
        }
    }
}
