using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win;
using DevExpress.Xpo;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.Win.Controllers
{
   public class MeasurePerformanceController : ViewController
    {
        public Stopwatch Stopwatch { get; set; }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            Frame.GetController<RefreshController>().RefreshAction.Executing += RefreshAction_Executing;
            Frame.GetController<RefreshController>().RefreshAction.Executed += RefreshAction_Executed; ;
            Frame.GetController<RecordsNavigationController>().Active[nameof(MeasurePerformanceController)] = false;
        }

        private void RefreshAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stopwatch = Stopwatch.StartNew();
        }

        private void RefreshAction_Executed(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
        {
            Stopwatch.Stop();
            var cnt = ObjectSpace.GetObjectsCount(View.ObjectTypeInfo.Type, null);

            var member = View.ObjectTypeInfo.Type.GetProperties().FirstOrDefault(m => m.GetCustomAttribute<AssociationAttribute>() != null);
            var cnt2 = ObjectSpace.GetObjectsCount(member.PropertyType.GetGenericArguments()[0], null);

            var mode = (View.Model as DevExpress.ExpressApp.Model.IModelListView).DataAccessMode;
            WinApplication.Messaging.Show("Elapsed", $"{View.Caption}{Environment.NewLine}DataAccessMode: {mode}{Environment.NewLine}{View.ObjectTypeInfo.Type.Name}: {cnt}{Environment.NewLine}{member.PropertyType.GetGenericArguments()[0].Name}: {cnt2}{Environment.NewLine}{Stopwatch.Elapsed}");
        }
    }
}
