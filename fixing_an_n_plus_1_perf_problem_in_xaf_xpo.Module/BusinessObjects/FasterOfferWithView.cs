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
    public class FasterOfferWithView : BaseObject, IOffer
    {
        public FasterOfferWithView(Session session) : base(session) { }
 
        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [Association, Aggregated]
        public XPCollection<FasterOfferItemWithView> OfferItems => GetCollection<FasterOfferItemWithView>(nameof(OfferItems));

        public void AddRange(IEnumerable<IOfferItem> items) => OfferItems.AddRange(items.OfType<FasterOfferItemWithView>());
    }

    public class FasterOfferItemWithView : BaseObject, IOfferItem
    {
        public FasterOfferItemWithView(Session session) : base(session) { }

        private int _Hours;
        [Persistent("Hours")]
        public int Hours { get => _Hours; set => SetPropertyValue(nameof(Hours), ref _Hours, value); }

        private FasterOfferWithView _FasterOfferWithView;
        [Persistent("FasterOfferWithView"), Association]
        public FasterOfferWithView FasterOfferWithView { get => _FasterOfferWithView; set => SetPropertyValue(nameof(FasterOfferWithView), ref _FasterOfferWithView, value); }
    }

    [DefaultProperty(nameof(HourSum))]
    public class FasterOfferWithViewQuery : BaseObject
    {
        public FasterOfferWithViewQuery(Session session) : base(session) { }

        private FasterOfferWithView _FasterOfferWithView;
        [Persistent("FasterOfferWithView")]
        [MemberDesignTimeVisibility(false)]
        [NoForeignKey]
        public FasterOfferWithView FasterOfferWithView { get => _FasterOfferWithView; set => SetPropertyValue(nameof(FasterOfferWithCQRS), ref _FasterOfferWithView, value); }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        private int _HourSum;
        [Persistent("HourSum")]
        public int HourSum { get => _HourSum; set => SetPropertyValue(nameof(HourSum), ref _HourSum, value); }        
    }
}