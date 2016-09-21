﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
using System.Windows.Forms;
using Proficy.CIMPLICITY;
using Proficy.CIMPLICITY.CimServer;

namespace JLR.SCADA.DCP
{
    public class CimpSeqs
    {
        public delegate void SeqHandler(Plc p, Sequence s);
        public event SeqHandler NewSeq;
        public delegate void PlcHandler(Plc p);
        public event PlcHandler NewPlc;

        public Dictionary<string, Plc> Plcs = new Dictionary<string, Plc>();
        public Dictionary<string, MachineSection> MachineSections = new Dictionary<string, MachineSection>();

        public string Project { get; set; }
        public string Class { get; set; }
        public bool Dynamic { get; set; }

        cimProject oProject = new cimProject();
        CimTable oTableLG_BAD_PART;
        CimTable oTableLG_CYCLE_TIME;
        CimTable oTableLG_FAULT_CODE;
        CimTable oTableLG_GOOD_PART;
        CimTable oTableLG_PART_TYPE;
        CimTable oTableLG_REASON_CODE;
        CimTable oTableLG_STATUS;

        public CimpSeqs(string project, string cimclass, bool dynamic)
        {
            Project = project;
            Class = cimclass;
            Dynamic = dynamic;

            oProject.OpenLocalProject(Project);
            oProject.ProjectUserName = "ADMINISTRATOR";
            oProject.ProjectPassword = "";
            oProject.dynamicMode = Dynamic;

            oTableLG_BAD_PART = oProject.Database.GetTable("LG_BAD_PART");
            oTableLG_CYCLE_TIME = oProject.Database.GetTable("LG_CYCLE_TIME");
            oTableLG_FAULT_CODE = oProject.Database.GetTable("LG_FAULT_CODE");
            oTableLG_GOOD_PART = oProject.Database.GetTable("LG_GOOD_PART");
            oTableLG_PART_TYPE = oProject.Database.GetTable("LG_PART_TYPE");
            oTableLG_REASON_CODE = oProject.Database.GetTable("LG_REASON_CODE");
            oTableLG_STATUS = oProject.Database.GetTable("LG_STATUS");


        }

        public void GetSeqences()
        {
            Plcs.Clear();

            foreach (CimObjectInstance o in oProject.Objects)
            {
                if (o.ClassID == Class)
                {
                    string plc = o.Attributes["PLC"].Value;
                    int ms = int.Parse(o.Attributes["MS"].Value.Substring(3, 2));
                    int seq = int.Parse(o.Attributes["SEQ"].Value);
                    string desc = o.Attributes["$DESCRIPTION"].Value;
                    string device = o.Attributes["$DEVICE_ID"].Value;
                    string zone = o.Attributes["$RESOURCE_ID"].Value;

                    Plc p = AddPLC(plc, device);

                    Sequence s = AddSequence(p, seq, zone, ms, desc);
                    NewSeq?.Invoke(p, s);

                }
            }
        }

        public void GetMachineSections()
        {
            foreach (CimAlarmClass c in oProject.AlarmClasses)
                if (c.ClassID.Substring(0, 2).Equals("MS"))
                {
                    MachineSection ms = new MachineSection(c.ClassID, c.Title);
                    MachineSections.Add(c.Title, ms);
                }
        }


        public Plc AddPLC(string description, string device)
        {
            if (this.Plcs.ContainsKey(description))
            {
                return this.Plcs[description];
            }
            else
            {
                int i = Plcs.Count + 1;
                Plc p = new Plc(i, description, device);
                Plcs.Add(p.Tag, p);
                NewPlc?.Invoke(p);
                return p;
            }
        }

        private Sequence AddSequence(Plc p, int seqNum, string zone, int ms, string description)
        {
            Sequence s = new Sequence(p, seqNum, zone, ms, description);
            p.Sequnces.Add(s.Key, s);
            return s;

        }

        public void ProjectDeleteSeq(Sequence s)
        {
            oProject.dynamicMode = Dynamic;
            if (oProject.Objects[s.ID] != null)
                oProject.Objects.Delete(s.ID, Dynamic);

            s.Plc.Sequnces.Remove(s.Key);
            if(s.Plc.Sequnces.Count == 0)
                Plcs.Remove(s.Plc.Tag);

            if (Dynamic)
            {
                Cimplicity.PointSet(@"IMPORT.NEW_SEQ_NAME", "Del: " + s.ID);
            }
        }

        public void ProjectAddSeq(Sequence s)
        {
            oProject.dynamicMode = Dynamic;
            CimObjectInstance oObj = new CimObjectInstance();
            oObj.ClassID = Class;
            if (Dynamic)
            {
                Cimplicity.PointSet(@"IMPORT.PROGRESS", $"Start Seq: {s.ID} for: {s.Plc.ObjRoot}");
            }

            oObj.ID = s.ID;
            oObj.Attributes.Set("PLC", s.Plc.Tag);
            oObj.Attributes.Set("SEQ", s.SeqNum.ToString());
            oObj.Attributes.Set("$ALARM_CLASS", this.MachineSections[s.ALARM_CLASS].ALARM_CLASS);
            oObj.Attributes.Set("$DESCRIPTION", s.Station);
            oObj.Attributes.Set("$DEVICE_ID", s.Plc.DEVICE_ID);                
            oObj.Attributes.Set("$RESOURCE_ID", "ZONE01");
            oObj.Attributes.Set("MS", "MS" + s.MS.ToString("000"));

            oObj.Routing.AddAll();

            oProject.Objects.Save(oObj, Dynamic);

            oTableLG_BAD_PART.AddLoggedPoint(s.ID + ".BAD_PART");
            oTableLG_CYCLE_TIME.AddLoggedPoint(s.ID + ".CYCLE_TIME");
            oTableLG_FAULT_CODE.AddLoggedPoint(s.ID + ".FAULT_CODE");
            oTableLG_GOOD_PART.AddLoggedPoint(s.ID + ".GOOD_PART");
            oTableLG_PART_TYPE.AddLoggedPoint(s.ID + ".PART_TYPE");
            oTableLG_REASON_CODE.AddLoggedPoint(s.ID + ".REASON_CODE");
            oTableLG_STATUS.AddLoggedPoint(s.ID + ".STATUS");

            if (Dynamic)
            {
                Cimplicity.PointSet(@"IMPORT.PROGRESS", $"End Seq: {s.ID} for: {s.Plc.ObjRoot}");
                Cimplicity.PointSet(@"IMPORT.NEW_SEQ_NAME", s.ID);
            }

            Plc p = AddPLC(s.Plc.Tag, s.Plc.DEVICE_ID);
            Sequence s1 = AddSequence(this.Plcs[s.Plc.Tag], s.SeqNum, "ZONE01", s.MS, s.Station);
            NewSeq?.Invoke(s1.Plc, s);


        }

    }
}
