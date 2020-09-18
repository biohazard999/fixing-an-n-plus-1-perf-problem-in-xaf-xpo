using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects
{
    public interface IOffer
    {
        string Name { get; set; }

        void AddRange(IEnumerable<IOfferItem> items);
    }

    public interface IOfferItem
    {
        int Hours { get; set; }
    }

    [DefaultClassOptions]
    public class SlowOffer : BaseObject, IOffer
    {
        public SlowOffer(Session session) : base(session) { }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [NonPersistent]
        public int HourSum
        {
            get
            {
                var sum = 0;
                foreach (var item in OfferItems)
                {
                    sum += item.Hours;
                }
                return sum;
            }
        }

        [Association, Aggregated]
        public XPCollection<SlowOfferItem> OfferItems => GetCollection<SlowOfferItem>(nameof(OfferItems));

        public void AddRange(IEnumerable<IOfferItem> items) => OfferItems.AddRange(items.OfType<SlowOfferItem>());
    }

    public class SlowOfferItem : BaseObject, IOfferItem
    {
        public SlowOfferItem(Session session) : base(session) { }

        private int _Hours;
        [Persistent("Hours")]
        public int Hours { get => _Hours; set => SetPropertyValue(nameof(Hours), ref _Hours, value); }

        private SlowOffer _SlowOffer;
        [Persistent("SlowOffer"), Association]
        public SlowOffer SlowOffer { get => _SlowOffer; set => SetPropertyValue(nameof(SlowOffer), ref _SlowOffer, value); }
    }
}