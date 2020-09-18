using System;
using System.Linq;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class SlowOfferWithPersistentAlias : BaseObject
    {
        public SlowOfferWithPersistentAlias(Session session) : base(session) { }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [PersistentAlias("OfferItems.Sum([Hours])")]
        public int ValueSum => (int)EvaluateAlias(nameof(ValueSum));

        [Association, Aggregated]
        public XPCollection<SlowOfferItemWithPersistentAlias> OfferItems => GetCollection<SlowOfferItemWithPersistentAlias>(nameof(OfferItems));
    }

    public class SlowOfferItemWithPersistentAlias : BaseObject
    {
        public SlowOfferItemWithPersistentAlias(Session session) : base(session) { }

        private int _Hours;
        [Persistent("Hours")]
        public int Hours { get => _Hours; set => SetPropertyValue(nameof(Hours), ref _Hours, value); }

        private SlowOfferWithPersistentAlias _SlowOfferWithPersistentAlias;
        [Persistent("SlowOfferWithPersistentAlias"), Association]
        public SlowOfferWithPersistentAlias SlowOfferWithPersistentAlias { get => _SlowOfferWithPersistentAlias; set => SetPropertyValue(nameof(SlowOfferWithPersistentAlias), ref _SlowOfferWithPersistentAlias, value); }
    }
}