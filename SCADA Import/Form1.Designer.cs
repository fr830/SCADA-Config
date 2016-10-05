namespace JLR.SCADA.DCP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPopulateProj = new System.Windows.Forms.Button();
            this.chkDynamic = new System.Windows.Forms.CheckBox();
            this.tvCimpConfig = new System.Windows.Forms.TreeView();
            this.chkSync = new System.Windows.Forms.CheckBox();
            this.btnGetPLCData = new System.Windows.Forms.Button();
            this.tvPlantConfig = new System.Windows.Forms.TreeView();
            this.btnAddSeq = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnDeleteSeq = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.cboSource = new System.Windows.Forms.ComboBox();
            this.btnClass = new System.Windows.Forms.Button();
            this.numZones = new System.Windows.Forms.NumericUpDown();
            this.tvMS = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numMS = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPopulateProj
            // 
            this.btnPopulateProj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPopulateProj.Location = new System.Drawing.Point(559, 317);
            this.btnPopulateProj.Name = "btnPopulateProj";
            this.btnPopulateProj.Size = new System.Drawing.Size(98, 23);
            this.btnPopulateProj.TabIndex = 8;
            this.btnPopulateProj.Text = "Get Cimp Data";
            this.btnPopulateProj.UseVisualStyleBackColor = true;
            this.btnPopulateProj.Click += new System.EventHandler(this.btnPopulateProj_Click);
            // 
            // chkDynamic
            // 
            this.chkDynamic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDynamic.AutoSize = true;
            this.chkDynamic.Location = new System.Drawing.Point(486, 321);
            this.chkDynamic.Name = "chkDynamic";
            this.chkDynamic.Size = new System.Drawing.Size(67, 17);
            this.chkDynamic.TabIndex = 5;
            this.chkDynamic.Text = "Dynamic";
            this.chkDynamic.UseVisualStyleBackColor = true;
            this.chkDynamic.CheckedChanged += new System.EventHandler(this.chkDynamic_CheckedChanged);
            // 
            // tvCimpConfig
            // 
            this.tvCimpConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCimpConfig.Location = new System.Drawing.Point(357, 12);
            this.tvCimpConfig.Name = "tvCimpConfig";
            this.tvCimpConfig.Size = new System.Drawing.Size(300, 299);
            this.tvCimpConfig.TabIndex = 9;
            this.tvCimpConfig.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvCimpConfig_Select);
            this.tvCimpConfig.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvCimpConfig_Select);
            this.tvCimpConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCimpConfig_Select);
            // 
            // chkSync
            // 
            this.chkSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSync.AutoSize = true;
            this.chkSync.Location = new System.Drawing.Point(158, 321);
            this.chkSync.Name = "chkSync";
            this.chkSync.Size = new System.Drawing.Size(50, 17);
            this.chkSync.TabIndex = 7;
            this.chkSync.Text = "Sync";
            this.chkSync.UseVisualStyleBackColor = true;
            this.chkSync.CheckedChanged += new System.EventHandler(this.chkSync_CheckedChanged);
            // 
            // btnGetPLCData
            // 
            this.btnGetPLCData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetPLCData.Location = new System.Drawing.Point(214, 317);
            this.btnGetPLCData.Name = "btnGetPLCData";
            this.btnGetPLCData.Size = new System.Drawing.Size(98, 23);
            this.btnGetPLCData.TabIndex = 8;
            this.btnGetPLCData.Text = "Get PLC Data";
            this.btnGetPLCData.UseVisualStyleBackColor = true;
            this.btnGetPLCData.Click += new System.EventHandler(this.btnGetPLCData_Click);
            // 
            // tvPlantConfig
            // 
            this.tvPlantConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvPlantConfig.Location = new System.Drawing.Point(12, 12);
            this.tvPlantConfig.Name = "tvPlantConfig";
            this.tvPlantConfig.Size = new System.Drawing.Size(300, 299);
            this.tvPlantConfig.TabIndex = 3;
            this.tvPlantConfig.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvPlantConfig_Select);
            this.tvPlantConfig.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvPlantConfig_Select);
            this.tvPlantConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPlantConfig_Select);
            // 
            // btnAddSeq
            // 
            this.btnAddSeq.Enabled = false;
            this.btnAddSeq.Location = new System.Drawing.Point(318, 77);
            this.btnAddSeq.Name = "btnAddSeq";
            this.btnAddSeq.Size = new System.Drawing.Size(33, 26);
            this.btnAddSeq.TabIndex = 11;
            this.btnAddSeq.Text = "->";
            this.btnAddSeq.UseVisualStyleBackColor = true;
            this.btnAddSeq.Click += new System.EventHandler(this.btnAddSeq_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(318, 29);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(33, 26);
            this.btnCompare.TabIndex = 12;
            this.btnCompare.Text = "<->";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnDeleteSeq
            // 
            this.btnDeleteSeq.Enabled = false;
            this.btnDeleteSeq.Location = new System.Drawing.Point(318, 199);
            this.btnDeleteSeq.Name = "btnDeleteSeq";
            this.btnDeleteSeq.Size = new System.Drawing.Size(33, 26);
            this.btnDeleteSeq.TabIndex = 13;
            this.btnDeleteSeq.Text = "<-";
            this.btnDeleteSeq.UseVisualStyleBackColor = true;
            this.btnDeleteSeq.Click += new System.EventHandler(this.btnDeleteSeq_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(318, 109);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(33, 26);
            this.btnAddAll.TabIndex = 14;
            this.btnAddAll.Text = ">>";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(318, 231);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(33, 26);
            this.btnDeleteAll.TabIndex = 14;
            this.btnDeleteAll.Text = "<<";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // cboSource
            // 
            this.cboSource.FormattingEnabled = true;
            this.cboSource.Items.AddRange(new object[] {
            "File",
            "Kepware",
            "Matrikon"});
            this.cboSource.Location = new System.Drawing.Point(31, 317);
            this.cboSource.Name = "cboSource";
            this.cboSource.Size = new System.Drawing.Size(121, 21);
            this.cboSource.TabIndex = 15;
            // 
            // btnClass
            // 
            this.btnClass.Location = new System.Drawing.Point(577, 563);
            this.btnClass.Name = "btnClass";
            this.btnClass.Size = new System.Drawing.Size(80, 23);
            this.btnClass.TabIndex = 16;
            this.btnClass.Text = "Add MS";
            this.btnClass.UseVisualStyleBackColor = true;
            this.btnClass.Visible = false;
            this.btnClass.Click += new System.EventHandler(this.btnClass_Click);
            // 
            // numZones
            // 
            this.numZones.Location = new System.Drawing.Point(410, 566);
            this.numZones.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numZones.Name = "numZones";
            this.numZones.Size = new System.Drawing.Size(33, 20);
            this.numZones.TabIndex = 17;
            this.numZones.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numZones.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numZones.Visible = false;
            // 
            // tvMS
            // 
            this.tvMS.Location = new System.Drawing.Point(357, 354);
            this.tvMS.Name = "tvMS";
            this.tvMS.Size = new System.Drawing.Size(300, 203);
            this.tvMS.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 568);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Zones";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 568);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "MS";
            this.label2.Visible = false;
            // 
            // numMS
            // 
            this.numMS.Location = new System.Drawing.Point(505, 566);
            this.numMS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMS.Name = "numMS";
            this.numMS.Size = new System.Drawing.Size(33, 20);
            this.numMS.TabIndex = 17;
            this.numMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMS.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numMS.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 598);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvMS);
            this.Controls.Add(this.numMS);
            this.Controls.Add(this.numZones);
            this.Controls.Add(this.btnClass);
            this.Controls.Add(this.cboSource);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnDeleteSeq);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnAddSeq);
            this.Controls.Add(this.tvPlantConfig);
            this.Controls.Add(this.btnGetPLCData);
            this.Controls.Add(this.chkSync);
            this.Controls.Add(this.tvCimpConfig);
            this.Controls.Add(this.chkDynamic);
            this.Controls.Add(this.btnPopulateProj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.numZones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView tvPlantConfig;
        private System.Windows.Forms.Button btnGetPLCData;
        private System.Windows.Forms.CheckBox chkSync;
        private System.Windows.Forms.TreeView tvCimpConfig;
        private System.Windows.Forms.CheckBox chkDynamic;
        private System.Windows.Forms.Button btnPopulateProj;
        private System.Windows.Forms.Button btnAddSeq;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnDeleteSeq;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.ComboBox cboSource;
        private System.Windows.Forms.Button btnClass;
        private System.Windows.Forms.NumericUpDown numZones;
        private System.Windows.Forms.TreeView tvMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMS;
    }
}

