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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnPopulateProj = new System.Windows.Forms.Button();
            this.chkDynamic = new System.Windows.Forms.CheckBox();
            this.tvCimpConfig = new System.Windows.Forms.TreeView();
            this.chkPLC = new System.Windows.Forms.CheckBox();
            this.chkSync = new System.Windows.Forms.CheckBox();
            this.btnGetPLCData = new System.Windows.Forms.Button();
            this.tvPlantConfig = new System.Windows.Forms.TreeView();
            this.btnAddSeq = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnDeleteSeq = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPopulateProj
            // 
            this.btnPopulateProj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPopulateProj.Location = new System.Drawing.Point(559, 563);
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
            this.chkDynamic.Location = new System.Drawing.Point(486, 567);
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
            this.tvCimpConfig.Size = new System.Drawing.Size(300, 545);
            this.tvCimpConfig.TabIndex = 9;
            this.tvCimpConfig.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvCimpConfig_Select);
            this.tvCimpConfig.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvCimpConfig_Select);
            this.tvCimpConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCimpConfig_Select);
            // 
            // chkPLC
            // 
            this.chkPLC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPLC.AutoSize = true;
            this.chkPLC.Location = new System.Drawing.Point(80, 567);
            this.chkPLC.Name = "chkPLC";
            this.chkPLC.Size = new System.Drawing.Size(72, 17);
            this.chkPLC.TabIndex = 6;
            this.chkPLC.Text = "PLC Data";
            this.chkPLC.UseVisualStyleBackColor = true;
            this.chkPLC.CheckedChanged += new System.EventHandler(this.chkPLC_CheckedChanged);
            // 
            // chkSync
            // 
            this.chkSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSync.AutoSize = true;
            this.chkSync.Location = new System.Drawing.Point(158, 567);
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
            this.btnGetPLCData.Location = new System.Drawing.Point(214, 563);
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
            this.tvPlantConfig.Size = new System.Drawing.Size(300, 545);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 598);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnDeleteSeq);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnAddSeq);
            this.Controls.Add(this.tvPlantConfig);
            this.Controls.Add(this.btnGetPLCData);
            this.Controls.Add(this.chkSync);
            this.Controls.Add(this.chkPLC);
            this.Controls.Add(this.tvCimpConfig);
            this.Controls.Add(this.chkDynamic);
            this.Controls.Add(this.btnPopulateProj);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView tvPlantConfig;
        private System.Windows.Forms.Button btnGetPLCData;
        private System.Windows.Forms.CheckBox chkSync;
        private System.Windows.Forms.CheckBox chkPLC;
        private System.Windows.Forms.TreeView tvCimpConfig;
        private System.Windows.Forms.CheckBox chkDynamic;
        private System.Windows.Forms.Button btnPopulateProj;
        private System.Windows.Forms.Button btnAddSeq;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnDeleteSeq;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnDeleteAll;
    }
}

