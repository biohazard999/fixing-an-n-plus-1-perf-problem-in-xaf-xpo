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
    public class FasterOfferWithLinq : BaseObject, IOffer
    {
        public FasterOfferWithLinq(Session session) : base(session) { }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [NonPersistent]
        public int HourSum => 
            Session.Query<FasterOfferItemWithLinq>()
            .Where(i => i.FasterOfferWithLinq.Oid == Oid)
            .Sum(m => m.Hours);

        [Association, Aggregated]
        public XPCollection<FasterOfferItemWithLinq> OfferItems => GetCollection<FasterOfferItemWithLinq>(nameof(OfferItems));

        public void AddRange(IEnumerable<IOfferItem> items) => OfferItems.AddRange(items.OfType<FasterOfferItemWithLinq>());
    }

    public class FasterOfferItemWithLinq : BaseObject, IOfferItem
    {
        public FasterOfferItemWithLinq(Session session) : base(session) { }

        private int _Hours;
        [Persistent("Hours")]
        public int Hours { get => _Hours; set => SetPropertyValue(nameof(Hours), ref _Hours, value); }

        private FasterOfferWithLinq _FasterOfferWithLinq;
        [Persistent("FasterOfferWithLinq"), Association]
        public FasterOfferWithLinq FasterOfferWithLinq { get => _FasterOfferWithLinq; set => SetPropertyValue(nameof(FasterOfferWithLinq), ref _FasterOfferWithLinq, value); }
    }
}