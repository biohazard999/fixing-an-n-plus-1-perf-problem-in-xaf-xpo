using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class RelatedData : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public RelatedData(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}


        private int _Value;
        [Persistent("Value")]
        public int Value { get => _Value; set => SetPropertyValue(nameof(Value), ref _Value, value); }

        private SlowObject _SlowObject;
        [Persistent("SlowObject")]
        public SlowObject SlowObject { get => _SlowObject; set => SetPropertyValue(nameof(SlowObject), ref _SlowObject, value); }

        private SlowObjectWithPersistentAlias _SlowObjectWithPersistentAlias;
        [Persistent("SlowObjectWithPersistentAlias")]
        public SlowObjectWithPersistentAlias SlowObjectWithPersistentAlias { get => _SlowObjectWithPersistentAlias; set => SetPropertyValue(nameof(SlowObjectWithPersistentAlias), ref _SlowObjectWithPersistentAlias, value); }

        private FastObject _FastObject;
        [Persistent("FastObject")]
        public FastObject FastObject { get => _FastObject; set => SetPropertyValue(nameof(FastObject), ref _FastObject, value); }
    }
}