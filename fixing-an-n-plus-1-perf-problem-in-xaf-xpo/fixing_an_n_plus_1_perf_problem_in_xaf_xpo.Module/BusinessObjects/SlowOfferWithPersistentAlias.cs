using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty(nameof(HourSum))]
    public class SlowOfferWithPersistentAlias : BaseObject, IOffer
    {
        public SlowOfferWithPersistentAlias(Session session) : base(session) { }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [PersistentAlias("OfferItems.Sum([Hours])")]
        public int HourSum => (int)EvaluateAlias(nameof(HourSum));

        [Association, Aggregated]
        public XPCollection<SlowOfferItemWithPersistentAlias> OfferItems => GetCollection<SlowOfferItemWithPersistentAlias>(nameof(OfferItems));

        public void AddRange(IEnumerable<IOfferItem> items) => OfferItems.AddRange(items.OfType<SlowOfferItemWithPersistentAlias>());
    }

    public class SlowOfferItemWithPersistentAlias : BaseObject, IOfferItem
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