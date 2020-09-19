using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty(nameof(HourSum))]
    public class FasterOffer : BaseObject, IOffer
    {
        public FasterOffer(Session session) : base(session) { }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [NonPersistent]
        public int HourSum
        {
            get
            {
                if (Session is IWideDataStorage storage)
                {
                    if (!storage.TryGetWideDataItem(GetType().FullName, out var _))
                    {
                        Dictionary<Guid, int> CalculateHours()
                            => Session.Query<FasterOffer>()
                                .Select(o => new
                                {
                                    Oid = o.Oid,
                                    HourSum = o.OfferItems.Sum(m => m.Hours)
                                })
                                .ToDictionary(o => o.Oid, o => o.HourSum);

                        storage.SetWideDataItem(GetType().FullName, CalculateHours());
                    }
                    if (storage.TryGetWideDataItem(GetType().FullName, out var store) && store is Dictionary<Guid, int> cache)
                    {
                        if (cache.TryGetValue(Oid, out var hourSum))
                        {
                            return hourSum;
                        }
                    }
                }
                //We never should reach this point
                //Do the right thing as fallback anyway
                return OfferItems.Sum(m => m.Hours);
            }
        }

        [Association, Aggregated]
        public XPCollection<FasterOfferItem> OfferItems => GetCollection<FasterOfferItem>(nameof(OfferItems));

        public void AddRange(IEnumerable<IOfferItem> items) => OfferItems.AddRange(items.OfType<FasterOfferItem>());
    }

    public class FasterOfferItem : BaseObject, IOfferItem
    {
        public FasterOfferItem(Session session) : base(session) { }

        private int _Hours;
        [Persistent("Hours")]
        public int Hours { get => _Hours; set => SetPropertyValue(nameof(Hours), ref _Hours, value); }

        private FasterOffer _FasterOffer;
        [Persistent("FasterOffer"), Association]
        public FasterOffer FasterOffer { get => _FasterOffer; set => SetPropertyValue(nameof(FasterOffer), ref _FasterOffer, value); }
    }
}