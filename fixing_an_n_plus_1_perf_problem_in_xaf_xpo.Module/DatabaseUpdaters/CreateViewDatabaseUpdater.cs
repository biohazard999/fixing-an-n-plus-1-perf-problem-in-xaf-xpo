using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.DatabaseUpdaters
{
    public class CreateViewDatabaseUpdater : ModuleUpdater
    {
        public CreateViewDatabaseUpdater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion)
        {
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            DropTable("FasterOfferWithViewQuery", true);
            ExecuteNonQueryCommand(@"DROP VIEW [dbo].[FasterOfferWithViewQuery]", true);

            ExecuteNonQueryCommand(@"CREATE VIEW [dbo].[FasterOfferWithViewQuery]
	AS select [FasterOfferWithView].[Oid] as Oid, 
	[FasterOfferWithView].[Oid] as [FasterOfferWithView], 
	[FasterOfferWithView].[Name] as [Name], 
	[FasterOfferWithView].[OptimisticLockField] as [OptimisticLockField], 
	[FasterOfferWithView].[GCRecord] AS [GCRecord],
	sum([FasterOfferItemWithView].[Hours]) AS [HourSum]
from FasterOfferWithView
  inner join FasterOfferItemWithView on [FasterOfferWithView].[Oid] = [FasterOfferItemWithView].[FasterOfferWithView]
group by [FasterOfferWithView].[Oid], [FasterOfferWithView].[Name], [FasterOfferWithView].[OptimisticLockField], [FasterOfferWithView].[GCRecord]
", true);

        }
    }
}
