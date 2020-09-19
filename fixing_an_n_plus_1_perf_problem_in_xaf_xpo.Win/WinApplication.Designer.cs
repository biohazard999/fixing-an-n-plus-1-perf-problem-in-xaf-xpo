namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Win {
    partial class fixing_an_n_plus_1_perf_problem_in_xaf_xpoWindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.module3 = new fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.fixing_an_n_plus_1_perf_problem_in_xaf_xpoModule();
            this.module4 = new fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.Win.fixing_an_n_plus_1_perf_problem_in_xaf_xpoWindowsFormsModule();
            this.viewVariantsModule1 = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // viewVariantsModule1
            // 
            this.viewVariantsModule1.ShowAdditionalNavigation = true;
            // 
            // fixing_an_n_plus_1_perf_problem_in_xaf_xpoWindowsFormsApplication
            // 
            this.ApplicationName = "fixing-an-n-plus-1-perf-problem-in-xaf-xpo";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.viewVariantsModule1);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.UseOldTemplates = false;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.fixing_an_n_plus_1_perf_problem_in_xaf_xpoWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.fixing_an_n_plus_1_perf_problem_in_xaf_xpoWindowsFormsApplication_CustomizeLanguagesList);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.fixing_an_n_plus_1_perf_problem_in_xaf_xpoModule module3;
        private fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.Win.fixing_an_n_plus_1_perf_problem_in_xaf_xpoWindowsFormsModule module4;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule1;
    }
}
