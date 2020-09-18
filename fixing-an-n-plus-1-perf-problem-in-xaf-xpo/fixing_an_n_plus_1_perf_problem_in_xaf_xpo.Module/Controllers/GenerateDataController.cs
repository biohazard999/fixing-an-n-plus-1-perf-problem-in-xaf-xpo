using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bogus;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;

using fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.Controllers
{
    public abstract class GenerateDataController<TObject, TAggregate> : ViewController
        where TObject : class, IOffer
        where TAggregate : class, IOfferItem
    {
        public ParametrizedAction GenerateData { get; }
        public GenerateDataController()
        {
            TargetObjectType = typeof(TObject);
            GenerateData = new ParametrizedAction(
                this,
                $"{GetType().FullName}.{nameof(GenerateData)}",
                DevExpress.Persistent.Base.PredefinedCategory.Edit,
                typeof(int)
            )
            {
                Caption = "Generate",
                Value = 200
            };

            GenerateData.Execute += GenerateData_Execute;
        }

        private void GenerateData_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            var batches = ((int)e.ParameterCurrentValue / 100);
            for (int i = 0; i < batches; i++)
            {
                using (var os = Application.CreateObjectSpace(typeof(SlowOffer)))
                {
                    var testOrders = new Faker<TAggregate>()
                        .CustomInstantiator((f) => os.CreateObject<TAggregate>())
                        .RuleFor(o => o.Hours, f => f.Random.Number(1, 10000));

                    var faker = new Faker<TObject>()
                        .CustomInstantiator((f) => os.CreateObject<TObject>())
                        .RuleFor(o => o.Name, (f, p) => f.Name.FirstName())
                        .FinishWith((f, o) => o.AddRange(testOrders.Generate(1000).ToList()));

                    faker.Generate(100);
                    os.CommitChanges();
                }
                ObjectSpace.Refresh();
            }            
        }
    }
}
