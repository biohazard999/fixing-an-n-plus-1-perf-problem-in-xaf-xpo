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
    public class SlowOfferWithLinq : BaseObject, IOffer
    {
        public SlowOfferWithLinq(Session session) : base(session) { }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [NonPersistent]
        public int HourSum => OfferItems.Sum(m => m.Hours);

        [Association, Aggregated]
        public XPCollection<SlowOfferItemWithLinq> OfferItems => GetCollection<SlowOfferItemWithLinq>(nameof(OfferItems));

        public void AddRange(IEnumerable<IOfferItem> items) => OfferItems.AddRange(items.OfType<SlowOfferItemWithLinq>());
    }

    public class SlowOfferItemWithLinq : BaseObject, IOfferItem
    {
        public SlowOfferItemWithLinq(Session session) : base(session) { }

        private int _Hours;
        [Persistent("Hours")]
        public int Hours { get => _Hours; set => SetPropertyValue(nameof(Hours), ref _Hours, value); }

        private SlowOfferWithLinq _SlowOfferWithLinq;
        [Persistent("SlowOfferWithLinq"), Association]
        public SlowOfferWithLinq SlowOfferWithLinq { get => _SlowOfferWithLinq; set => SetPropertyValue(nameof(SlowOfferWithLinq), ref _SlowOfferWithLinq, value); }
    }
}