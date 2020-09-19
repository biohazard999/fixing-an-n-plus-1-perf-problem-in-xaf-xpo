using System;
using System.Collections.Generic;
using System.Linq;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;

using fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.DatabaseUpdaters;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module
{
    public sealed partial class fixing_an_n_plus_1_perf_problem_in_xaf_xpoModule : ModuleBase
    {
        public fixing_an_n_plus_1_perf_problem_in_xaf_xpoModule()
        {
            InitializeComponent();
            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) => new ModuleUpdater[]
        {
            new CreateViewDatabaseUpdater(objectSpace, versionFromDB)
        };
    }
}
