using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class FasterOfferWithCQRS : BaseObject, IOffer
    {
        public FasterOfferWithCQRS(Session session) : base(session) { }

        protected override void OnSaving()
        {
            var query = Session.FindObject<FasterOfferWithCQRSQuery>(PersistentCriteriaEvaluationBehavior.InTransaction, new BinaryOperator(nameof(FasterOfferWithCQRSQuery.FasterOfferWithCQRS), this, BinaryOperatorType.Equal));

            if (IsDeleted && query != null)
            {
                Session.Delete(query);
            }
            if (query == null)
            {
                query = new FasterOfferWithCQRSQuery(Session);
            }
            if (query != null)
            {
                query.FasterOfferWithCQRS = this;
                query.Name = Name;
                query.HourSum = OfferItems.Sum(o => o.Hours);
            }
            base.OnSaving();
        }
        
        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        [Association, Aggregated]
        public XPCollection<FasterOfferItemWithCQRS> OfferItems => GetCollection<FasterOfferItemWithCQRS>(nameof(OfferItems));

        public void AddRange(IEnumerable<IOfferItem> items) => OfferItems.AddRange(items.OfType<FasterOfferItemWithCQRS>());
    }

    public class FasterOfferItemWithCQRS : BaseObject, IOfferItem
    {
        public FasterOfferItemWithCQRS(Session session) : base(session) { }

        protected override void OnSaving()
        {
            FasterOfferWithCQRS?.Save();
            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            FasterOfferWithCQRS?.Save();
            base.OnDeleting();
        }

        private int _Hours;
        [Persistent("Hours")]
        public int Hours { get => _Hours; set => SetPropertyValue(nameof(Hours), ref _Hours, value); }

        private FasterOfferWithCQRS _FasterOfferWithCQRS;
        [Persistent("FasterOfferWithCQRS"), Association]
        public FasterOfferWithCQRS FasterOfferWithCQRS { get => _FasterOfferWithCQRS; set => SetPropertyValue(nameof(FasterOfferWithCQRS), ref _FasterOfferWithCQRS, value); }
    }

    [DefaultProperty(nameof(HourSum))]
    public class FasterOfferWithCQRSQuery : BaseObject
    {
        public FasterOfferWithCQRSQuery(Session session) : base(session) { }

        private FasterOfferWithCQRS _FasterOfferWithCQRS;
        [Persistent("FasterOfferWithCQRS")]
        [MemberDesignTimeVisibility(false)]
        public FasterOfferWithCQRS FasterOfferWithCQRS { get => _FasterOfferWithCQRS; set => SetPropertyValue(nameof(FasterOfferWithCQRS), ref _FasterOfferWithCQRS, value); }

        private string _Name;
        [Persistent("Name")]
        public string Name { get => _Name; set => SetPropertyValue(nameof(Name), ref _Name, value); }

        private int _HourSum;
        [Persistent("HourSum")]
        public int HourSum { get => _HourSum; set => SetPropertyValue(nameof(HourSum), ref _HourSum, value); }        
    }
}