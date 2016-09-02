using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
using System.Windows.Forms;
using System.IO;

namespace JLR.SCADA.DCP
{
    public class Plant
    {
        public bool Sync { get; set; }
        public delegate void SeqHandler(Plc p, Sequence s);
        public event SeqHandler NewSeq;
        public delegate void PlcHandler(Plc p);
        public event PlcHandler NewPlc;

        public Dictionary<string, Plc> Plcs = new Dictionary<string, Plc>();

        OPCServer oServer = new OPCServer();
        OPCGroup oGroup;
        OPCBrowser oBrowser;
        string sGroup = "KJR";

        private void OCPCallBack(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Values, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            Plc p = Plcs.ElementAt(TransactionID - 1).Value;
            string[] v = new string[61];

            for (int i = 1; i <= 60; i++)
            {
                v[int.Parse(ClientHandles.GetValue(i).ToString())] = Values.GetValue(i).ToString();

            }

            for (int i = 1; i <= 30; i++)
            {
                int ms = int.Parse(v[i]);
                string desc = v[i + 30];
                if (ms > 0)
                {
                    Sequence s = p.AddSequence(i, ms, desc);
                    NewSeq?.Invoke(p, s);

                }
            }
        }

        public Plant() { }

        public void GetOPCTags(string server, bool SyncData)
        {
            Plcs.Clear();
            this.Sync = SyncData;
            List<string> sChannels = new List<string>();

            oServer.Connect(server);
            oServer.OPCGroups.DefaultGroupIsActive = true;
            int iChannels;
                       
            oGroup = oServer.OPCGroups.Add(sGroup);
            oGroup.IsSubscribed = true;
            oGroup.OPCItems.DefaultIsActive = true;
            oGroup.AsyncReadComplete += OCPCallBack;

            oBrowser = oServer.CreateBrowser();
            oBrowser.Filter = "A3*";
            oBrowser.ShowBranches();

            iChannels = oBrowser.Count;

            for (int i = 0; i < iChannels; i++)
                sChannels.Add(oBrowser.Item(i + 1));

            foreach (string sChannel in sChannels)
            {
                oBrowser.MoveDown(sChannel);
                oBrowser.Filter = "R*";
                oBrowser.ShowBranches();

                for (int j = 0; j < oBrowser.Count; j++)
                    this.AddPLC(sChannel + "." + oBrowser.Item(j + 1));

                oBrowser.MoveUp();
            }
        }

        public void GetTestData()
        {
            Plcs.Clear();

            string filePath = @"TestPlcs.csv";
            StreamReader sr = new StreamReader(filePath);
            //var lines = new List<string[]>();
            sr.ReadLine().Split(',');

            while (!sr.EndOfStream)
            {
                string[] Line = sr.ReadLine().Split(',');
                Plc p;

                if (Plcs.ContainsKey(Line[0]))
                    p = Plcs[Line[0]];

                else
                    p = _AddPLC(Line[0]);


                int ms = int.Parse(Line[2]);
                if (ms > 0)
                {
                    Sequence s = p.AddSequence(int.Parse(Line[1]), ms, Line[3]);
                    NewSeq?.Invoke(p, s);
                }
            }
            sr.Close();
        }
        
        private Plc _AddPLC(string description)
        {
   
            int i = Plcs.Count + 1;
            Plc p = new Plc(i,description);

            Plcs.Add(description, p);
            NewPlc?.Invoke(p);
            return p;
        }

        public Plc AddPLC(string description)
        {
            int i = Plcs.Count + 1;
            Plc p = new Plc(i, description);
            Plcs.Add(description, p);
            NewPlc?.Invoke(p);
            GetSeqences(p);
            return p;
        }

        private void GetSeqences(Plc plc)
        {
            Array ItemIDs = Array.CreateInstance(typeof(string), 61);
            Array ClntHndl = Array.CreateInstance(typeof(int), 61);

            Array SvrHndl;
            Array SvrErr;
            Array Values;
            object qual = new object();
            object TS = new object();

            for (int i = 1; i <= 30; i++)
            {
                ItemIDs.SetValue($"{plc.Description}.ZZMISSEQ[{(i).ToString()}].SEQ", i);
                ItemIDs.SetValue($"{plc.Description}.ZZMISSEQ[{(i).ToString()}].DESC.DATA/32", i + 30);
                ClntHndl.SetValue(i, i);
                ClntHndl.SetValue(i + 30, i + 30);
            }
            try
            {
                oGroup.OPCItems.AddItems((int)60, ref ItemIDs, ref ClntHndl, out SvrHndl, out SvrErr);
                if (this.Sync)
                {
                    oGroup.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, (int)60, ref SvrHndl, out Values, out SvrErr, out qual, out TS);

                    for (int j = 1; j <= 30; j++)
                    {
                        int ms = int.Parse(Values.GetValue(j).ToString());
                        string desc = Values.GetValue(j + 30).ToString();
                        if (ms > 0)
                        {
                            Sequence s = plc.AddSequence(j, ms, desc);
                            NewSeq?.Invoke(plc, s);
                        }
                    }

                    oGroup.OPCItems.Remove(60, ref SvrHndl, out SvrErr);
                }
                else
                                {
                    int ic;
                    oGroup.AsyncRead(60, ref SvrHndl, out SvrErr, plc.ID, out ic);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
    }
}
