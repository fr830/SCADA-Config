using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace JLR.SCADA.DCP
{
    public partial class Form1 : Form
    {
        bool Dynamic = false;
        bool SyncData = false;
        string sPrjName = "SCADA_TEST";
        string sPrjPath = "D:\\Projects\\";
        string sClass = "SCADA_SEQ";
        Plant plant;
        CimpSeqs cimp;

        public Form1()
        {
            InitializeComponent();

            plant = new Plant();
            plant.NewSeq += NewPlantSeq;
            plant.NewPlc += NewPlantPLC;

            cimp = new CimpSeqs(sPrjName, sPrjPath, sClass, Dynamic);
            cimp.NewSeq += NewCimpSeq;
            cimp.NewPlc += NewCimpPLC;
            cimp.NewMS += NewCimpMS;
            
            chkDynamic.Checked = Dynamic;
        }

        public void NewPlantPLC(Plc p)
        {

            tvPlantConfig.Nodes.Add(p.Tag, p.ObjRoot).ForeColor = Color.DarkRed;
            tvPlantConfig.Refresh();
        }

        public void NewPlantSeq(Plc p, Sequence s)
        {

            TreeNode[] t1 = tvPlantConfig.Nodes.Find(p.Tag, true);

            if (t1.Length == 1)
            {
                t1[0].Nodes.Add(s.Key, s.Description);
                t1[0].ForeColor = Color.Black;
                tvPlantConfig.Refresh();
            }
            tvPlantConfig.Refresh();
        }
        
        public void NewCimpMS(MachineSection ms)
        {
            if (tvMS.Nodes.Find(ms.Zone, true).Count() == 0)
                tvMS.Nodes.Add(ms.Zone, ms.Zone);

            tvMS.Nodes[ms.Zone].Nodes.Add(ms.ALARM_CLASS, ms.MS);
            //tvMS.Nodes.Add(ms.MS, ms.Description);
            tvMS.Refresh();
        }
        public void NewCimpPLC(Plc p)
        {

            tvCimpConfig.Nodes.Add(p.Tag, p.ObjRoot).ForeColor = Color.DarkRed;
            tvCimpConfig.Refresh();
        }

        public void NewCimpSeq(Plc p, Sequence s)
        {
            TreeNode[] t1 = tvCimpConfig.Nodes.Find(p.Tag, true);
            TreeNode t2;

            if (t1.Length == 1)
            {
                t2 = t1[0];
                t2.Nodes.Add(s.Key, s.Description);
                t2.ForeColor = Color.Black;
                tvPlantConfig.Refresh();
                tvCimpConfig.Refresh();

            }
        }

        private void chkDynamic_CheckedChanged(object sender, EventArgs e)
        {
            Dynamic = chkDynamic.Checked;
            cimp.Dynamic = Dynamic;
        }

        private void btnGetPLCData_Click(object sender, EventArgs e)
        {
            tvPlantConfig.Nodes.Clear();
            switch(cboSource.Text)
            {
                case "Kepware":
                    plant.GetOPCTags(Plant.OPCType.Kepware, SyncData);
                    break;

                case "Matrikon":
                    plant.GetOPCTags(Plant.OPCType.Matrikon, SyncData);
                    break;

                case "File":
                    plant.GetTestData();
                    break;
            }
        }

        private void chkSync_CheckedChanged(object sender, EventArgs e)
        {
            SyncData = chkSync.Checked;
        }

        private void btnPopulateProj_Click(object sender, EventArgs e)
        {
            tvCimpConfig.Nodes.Clear();
            tvMS.Nodes.Clear();
            cimp.GetMachineSections();
            cimp.GetSeqences();
        }

        private void btnAddSeq_Click(object sender, EventArgs e)
        {
            TreeNode t = tvPlantConfig.SelectedNode;
            switch (t.Level)
            {
                case 0:
                    foreach (TreeNode t1 in t.Nodes)
                        if (t1.BackColor.Equals(Color.DarkRed))
                        {
                            if (tvCimpConfig.Nodes.Find(t1.Name, true).Count() == 1)
                                tvCimpConfig.Nodes.Remove(tvCimpConfig.Nodes.Find(t1.Name, true)[0]);

                            cimp.ProjectAddSeq(plant.Plcs[t.Name].Sequnces[t1.Name]);
                        }
                    break;

                case 1:
                    if (tvCimpConfig.Nodes.Find(t.Name, true).Count() == 1)
                        tvCimpConfig.Nodes.Remove(tvCimpConfig.Nodes.Find(t.Name, true)[0]);

                    cimp.ProjectAddSeq(plant.Plcs[t.Parent.Name].Sequnces[t.Name]);
                    break;
            }

            Compare();
        }

        private void Compare()
        {
            tvCimpConfig.Sort();
            tvPlantConfig.Sort();

            tvCimpConfig.Refresh();
            tvPlantConfig.Refresh();

            foreach (TreeNode t1 in tvPlantConfig.Nodes)
                foreach (TreeNode t2 in t1.Nodes)
                {
                    t2.BackColor = Color.DarkRed;
                    t2.ForeColor = Color.White;
                }
            foreach (TreeNode t1 in tvCimpConfig.Nodes)
                foreach (TreeNode t2 in t1.Nodes)
                {
                    t2.BackColor = Color.DarkRed;
                    t2.ForeColor = Color.White;
                }

            tvCimpConfig.Refresh();
            tvPlantConfig.Refresh();

            foreach (KeyValuePair<string, Plc> p1 in plant.Plcs)
            {
                tvPlantConfig.Nodes.Find(p1.Key, true)[0].BackColor = Color.Transparent;
                tvPlantConfig.Nodes.Find(p1.Key, true)[0].ForeColor = Color.Black;

                if (cimp.Plcs.ContainsKey(p1.Key))
                {
                    foreach (KeyValuePair<string, Sequence> s1 in p1.Value.Sequnces)
                        if (cimp.Plcs[p1.Key].Sequnces.ContainsKey(s1.Key))
                            if(cimp.Plcs[p1.Key].Sequnces[s1.Key].MS == plant.Plcs[p1.Key].Sequnces[s1.Key].MS)
                                {
                                    tvCimpConfig.Nodes.Find(s1.Key, true)[0].BackColor = Color.Transparent;
                                    tvCimpConfig.Nodes.Find(s1.Key, true)[0].ForeColor = Color.Black;
                                }
                                else
                                {
                                    tvPlantConfig.Nodes.Find(p1.Key, true)[0].BackColor = Color.DarkRed;
                                    tvPlantConfig.Nodes.Find(p1.Key, true)[0].ForeColor = Color.White;
                                }
                }
                else
                {
                    tvPlantConfig.Nodes.Find(p1.Key, true)[0].BackColor = Color.DarkRed;
                    tvPlantConfig.Nodes.Find(p1.Key, true)[0].ForeColor = Color.White;
                }
            }

            tvCimpConfig.Refresh();
            tvPlantConfig.Refresh();

            foreach (KeyValuePair<string, Plc> p1 in cimp.Plcs)
            {
                tvCimpConfig.Nodes.Find(p1.Key, true)[0].BackColor = Color.Transparent;
                tvCimpConfig.Nodes.Find(p1.Key, true)[0].ForeColor = Color.Black;

                if (plant.Plcs.ContainsKey(p1.Key))
                {
                    foreach (KeyValuePair<string, Sequence> s1 in p1.Value.Sequnces)
                        if (plant.Plcs[p1.Key].Sequnces.ContainsKey(s1.Key))
                            if (plant.Plcs[p1.Key].Sequnces[s1.Key].MS == cimp.Plcs[p1.Key].Sequnces[s1.Key].MS)
                            {
                                tvPlantConfig.Nodes.Find(s1.Key, true)[0].BackColor = Color.Transparent;
                                tvPlantConfig.Nodes.Find(s1.Key, true)[0].ForeColor = Color.Black;
                            }
                            else
                            {
                                tvPlantConfig.Nodes.Find(p1.Key, true)[0].BackColor = Color.DarkRed;
                                tvPlantConfig.Nodes.Find(p1.Key, true)[0].ForeColor = Color.White;
                            }

                }
                else
                {
                    tvCimpConfig.Nodes.Find(p1.Key, true)[0].BackColor = Color.DarkRed;
                    tvCimpConfig.Nodes.Find(p1.Key, true)[0].ForeColor = Color.White;
                }
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            Compare();
        }

        private void tvCimpConfig_Select(object sender, TreeViewEventArgs e)
        {

            string s = e.Node.Name;
            int level = e.Node.Level;

            TreeNode[] t = tvPlantConfig.Nodes.Find(s, true);

            switch (e.Action)
            {
                case TreeViewAction.Expand:
                    if (t.Length == 1 && level == 0)
                        t[0].ExpandAll();
                    break;

                case TreeViewAction.Collapse:
                    if (t.Length == 1 && level == 0)
                        t[0].Collapse();
                    break;

                case TreeViewAction.ByMouse:
                case TreeViewAction.ByKeyboard:
                    btnAddSeq.Enabled = false;
                    switch (level)
                    {
                        case 0:
                        case 1:
                            btnDeleteSeq.Enabled = true;// e.Node.BackColor.Equals(Color.DarkRed);
                            break;


                    }

                    break;

            }
        }

        private void tvPlantConfig_Select(object sender, TreeViewEventArgs e)
        {
            string s = e.Node.Name;
            int level = e.Node.Level;

            TreeNode[] t = tvCimpConfig.Nodes.Find(s, true);

            switch (e.Action)
            {
                case TreeViewAction.Expand:
                    if (t.Length == 1 && level == 0)
                        t[0].ExpandAll();
                    break;

                case TreeViewAction.Collapse:
                    if (t.Length == 1 && level == 0)
                        t[0].Collapse();
                    break;

                case TreeViewAction.ByMouse:
                case TreeViewAction.ByKeyboard:
                    switch (level)
                    {
                        case 0:
                        case 1:
                            btnAddSeq.Enabled = e.Node.BackColor.Equals(Color.DarkRed);
                            break;

                    }
                    break;

            }
        }

        private void btnDeleteSeq_Click(object sender, EventArgs e)
        {
            TreeNode t = tvCimpConfig.SelectedNode;
            List<TreeNode> toDel = new List<TreeNode>();

            switch (t.Level)
            {
                case 0:
                    foreach (TreeNode t1 in t.Nodes)
                        toDel.Add(t1);

                    foreach (TreeNode t2 in toDel)
                    {
                        cimp.ProjectDeleteSeq(cimp.Plcs[t.Name].Sequnces[t2.Name]);
                        t.Nodes.Remove(t2);
                        tvCimpConfig.Refresh();
                    }

                    if (t.Nodes.Count == 0)
                    {
                        tvCimpConfig.Nodes.Remove(t);
                        tvCimpConfig.Refresh();
                    }

                    break;

                case 1:
                    TreeNode t3 = t.Parent;

                    cimp.ProjectDeleteSeq(cimp.Plcs[t3.Name].Sequnces[t.Name]);
                    tvCimpConfig.Nodes.Remove(t);

                    if (t3.Nodes.Count == 0)
                        tvCimpConfig.Nodes.Remove(t3);
                    break;
            }

            Compare();
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode t in tvPlantConfig.Nodes)
                foreach (TreeNode t1 in t.Nodes)
                    if (t1.BackColor.Equals(Color.DarkRed))
                    {
                        if (tvCimpConfig.Nodes.Find(t1.Name, true).Count() == 1)
                            tvCimpConfig.Nodes.Remove(tvCimpConfig.Nodes.Find(t1.Name, true)[0]);
                        cimp.ProjectAddSeq(plant.Plcs[t.Name].Sequnces[t1.Name]);
                    }

            Compare();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            List<TreeNode> toDel = new List<TreeNode>();
            foreach (TreeNode t in tvCimpConfig.Nodes)
                foreach (TreeNode t1 in t.Nodes)
                    toDel.Add(t1);


            foreach (TreeNode t2 in toDel)

            {
                TreeNode t3 = t2.Parent;
                cimp.ProjectDeleteSeq(cimp.Plcs[t3.Name].Sequnces[t2.Name]);
                t3.Nodes.Remove(t2);
                tvCimpConfig.Refresh();
                if (t3.Nodes.Count == 0)
                {
                    tvCimpConfig.Nodes.Remove(t3);
                    tvCimpConfig.Refresh();
                }

            }

            Compare();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 2000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.btnAddSeq, "Add Selected Seqs");
            toolTip1.SetToolTip(this.btnAddAll, "Add All Seqs");
            toolTip1.SetToolTip(this.btnCompare, "Compare");
            toolTip1.SetToolTip(this.btnDeleteAll, "Delete All Objects");
            toolTip1.SetToolTip(this.btnDeleteSeq, "Delete Selected Object");

        }

        private void btnClass_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (int z = 1; z <= (int)numZones.Value; z++)
            {
                cimp.ProjectAddResource("ZONE" + z.ToString("00"));
                for (int m = 1; m <= (int)numMS.Value; m++)
                {
                    ++i;
                    cimp.ProjectAddAlmCls(i, "ZONE" + z.ToString("00"), "MS" + m.ToString("00"));
                }
            }

            numZones.Visible = false;
            numMS.Visible = false;
            btnClass.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            cimp.GetMachineSections();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            numZones.Visible = true;
            btnClass.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            numMS.Visible = true;
        }
    }
}
