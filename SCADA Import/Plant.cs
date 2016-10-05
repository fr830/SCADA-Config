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
        public enum OPCType {Kepware, Matrikon}
        public bool Sync { get; set; }
        public delegate void SeqHandler(Plc p, Sequence s);
        public event SeqHandler NewSeq;
        public delegate void PlcHandler(Plc p);
        public event PlcHandler NewPlc;

        public readonly Dictionary<string, Plc> Plcs = new Dictionary<string, Plc>();


        private OPCServer oServer = new OPCServer();
        private OPCGroup oGroup;
        private OPCBrowser oBrowser;
        private string sGroup = "SCADA";
        private OPCType type;

        private void OCPCallBack(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Values, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            Plc p = Plcs.ElementAt(TransactionID - 1).Value;
            string[] v = new string[61];
            string zone = "ZONE" + TransactionID.ToString("00");

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
                    Sequence s = p.AddSequence(i, zone, ms, desc);
                    NewSeq?.Invoke(p, s);

                }
            }
        }

        public Plant() { }

        public void GetOPCTags(OPCType opc, bool SyncData)
        {
            string server = "";
            string filter1 = "";
            string filter2 = "";
            string sep = "";
            string device = "";
            type = opc;

            switch (type)
            {
                case OPCType.Kepware:
                    server = "Kepware.KEPServerEX.V5";
                    filter1 = "A3*";
                    filter2 = "R*";
                    sep = ".";
                    device = "KEPWARE";
                    break;
                case OPCType.Matrikon:
                    server = "Matrikon.OPC.AllenBradleyPLCs.1";
                    filter1 = "A3*";
                    filter2 = "";
                    sep = ":";
                    device = "MATRIKON";
                    break;
            }

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
            oBrowser.Filter = filter1;
            oBrowser.ShowBranches();

            iChannels = oBrowser.Count;

            for (int i = 0; i < iChannels; i++)
                sChannels.Add(oBrowser.Item(i + 1));

            foreach (string sChannel in sChannels)
            {
                oBrowser.MoveDown(sChannel);
                oBrowser.Filter = filter2;
                oBrowser.ShowBranches();

                for (int j = 0; j < oBrowser.Count; j++)
                    this.AddPLC(sChannel + sep + oBrowser.Item(j + 1), device);

                oBrowser.MoveUp();
            }
        }

        public void GetTestData()
        {
            Plcs.Clear();

            string filePath = @"TestPlcs.csv";
            StreamReader sr = new StreamReader(filePath);
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
                    Sequence s = p.AddSequence(int.Parse(Line[1]), Line[4], ms, Line[3]);
                    NewSeq?.Invoke(p, s);
                }
            }
            sr.Close();
        }
        
        private Plc _AddPLC(string description)
        {
            int i = Plcs.Count + 1;
            Plc p = new Plc(i,description,"KEPWARE");

            Plcs.Add(description, p);
            NewPlc?.Invoke(p);
            return p;
        }

        public Plc AddPLC(string description, string device)
        {
            int i = Plcs.Count + 1;
            Plc p = new Plc(i, description, device);
            Plcs.Add(p.Tag, p);
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
            string tag = "";

            try
            {
                for (int i = 1; i <= 30; i++)
                {
                    switch (type)
                    {
                        case OPCType.Kepware:

                            tag = $"{plc.Tag}.ZZMISSEQ[{(i).ToString()}].SEQ";
                            ItemIDs.SetValue(tag, i);
                            tag = $"{plc.Tag}.ZZMISSEQ[{(i).ToString()}].DESC.DATA/8";
                            ItemIDs.SetValue(tag, i + 30);
                            break;

                        case OPCType.Matrikon:
                            tag = $"{plc.Tag}:PLC:SCADA_CONFIG:ZZMISSEQ[{(i).ToString()}].SEQ.VALUE";
                            ItemIDs.SetValue(tag, i);
                            //"A3_01_R01_S01: PLC: SCADA_CONFIG: ZZMISSEQ[1].DESC.VALUE"
                            tag = $"{plc.Tag}:PLC:SCADA_CONFIG:ZZMISSEQ[{(i).ToString()}].DESC.VALUE";
                            ItemIDs.SetValue(tag, i + 30);
                            break;
                    }

                    ClntHndl.SetValue(i, i);
                    ClntHndl.SetValue(i + 30, i + 30);
                }

                oGroup.OPCItems.AddItems(60, ref ItemIDs, ref ClntHndl, out SvrHndl, out SvrErr);
                if (this.Sync)
                {
                    oGroup.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, 60, ref SvrHndl, out Values, out SvrErr, out qual, out TS);

                    string zone = "ZONE" + plc.ID.ToString("00");

                    for (int j = 1; j <= 30; j++)
                    {
                        int ms = int.Parse(Values.GetValue(j).ToString());
                        string desc = Values.GetValue(j + 30).ToString();
                        if (ms > 0)
                        {
                            Sequence s = plc.AddSequence(j, zone, ms, desc);
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
            catch (Exception ex) { MessageBox.Show(ex.ToString());}

        }
    }
}
