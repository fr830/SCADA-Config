using System;
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
        public delegate void MSHandler(MachineSection m);
        public event MSHandler NewMS;

        public Dictionary<string, Plc> Plcs = new Dictionary<string, Plc>();
        public Dictionary<string, MachineSection> MachineSections = new Dictionary<string, MachineSection>();

        public string Project { get; set; }
        public string Path { get; set; }
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

        public CimpSeqs(string prjName, string prjPath, string cimclass, bool dynamic)
        {
            Project = prjName;
            Path = prjPath;
            Class = cimclass;
            Dynamic = dynamic;

            oProject.OpenLocalProject($"{prjPath}{prjName}\\{prjName}.gef");
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
                    int ms = int.Parse(o.Attributes["MS"].Value.Substring(2, 2));
                    string MS = o.Attributes["AL"].Value;
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
            MachineSections.Clear();
            foreach (CimObjectInstance c in oProject.Objects)
                if (c.ClassID.Equals("SCADA_MS"))
                {
                    MachineSection ms = new MachineSection(c.ID, c.Description, c.Attributes["MS"].Value);
                    MachineSections.Add(c.ID, ms);
                    NewMS?.Invoke(ms);
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

            if (oProject.Objects[s.ID] != null)
                Plcs[s.Plc.Tag].Sequnces.Remove(s.Key) ;

            CimObjectInstance oObj = new CimObjectInstance();
            oObj.ClassID = Class;
            if (Dynamic)
            {
                Cimplicity.PointSet(@"IMPORT.PROGRESS", $"Start Seq: {s.ID} for: {s.Plc.ObjRoot}");
            }

            oObj.ID = s.ID;
            oObj.Attributes.Set("$DESCRIPTION", s.Station);
            oObj.Attributes.Set("$DEVICE_ID", s.Plc.DEVICE_ID);
            oObj.Attributes.Set("$RESOURCE_ID", $"ZONE0{s.Plc.ID.ToString()}");
            oObj.Attributes.Set("AL", this.MachineSections[s.msObj].ALARM_CLASS);
            oObj.Attributes.Set("MS", this.MachineSections[s.msObj].MS);
            oObj.Attributes.Set("PLC", s.Plc.Tag);
            oObj.Attributes.Set("SEQ", s.SeqNum.ToString());

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
            Sequence s1 = AddSequence(this.Plcs[s.Plc.Tag], s.SeqNum, $"ZONE0{s.Plc.ID.ToString()}", s.MS, s.Station);
            NewSeq?.Invoke(s1.Plc, s);


        }

        public void ProjectAddAlmCls(int i, string zone, string ms)
        {
            CimAlarmClass c;

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}A");
            c.Title = $"Auto {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_15_Yellow;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}S");
            c.Title = $"Starved {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_15_Yellow;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}B");
            c.Title = $"Blocked {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_15_Yellow;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}P");
            c.Title = $"P Hold {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_15_Yellow;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}W");
            c.Title = $"Waiting Attention {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_1_Red;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}R");
            c.Title = $"Repair in Progress {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_4_Maroon;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}M");
            c.Title = $"Shutdown {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_3_Blue;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();

            c = oProject.AlarmClasses.Add($"MS{i.ToString("00")}E");
            c.Title = $"E-Stop {zone}  {ms}";
            c.AlarmHiHiBGColor = CimAlarmClassColorEnum.cimColorIndx_1_Red;
            c.AlarmHiHiFGColor = CimAlarmClassColorEnum.cimColorIndx_0_Black;
            c.Save();
            
            CimObjectInstance oObj = new CimObjectInstance();
            oObj.ClassID = "SCADA_MS";

            oObj.ID = zone + "_" + ms;
            oObj.Attributes.Set("$RESOURCE_ID", zone);
            oObj.Attributes.Set("$DESCRIPTION", zone + " " + ms);
            oObj.Attributes.Set("MS", $"MS{i.ToString("00")}");

            oObj.Routing.AddAll();

            oProject.Objects.Save(oObj, Dynamic);

        }

        public void ProjectAddResource(string zone)
        {
            cimResource r;
            r = oProject.Resources.New(zone);
            r.Users.AddAll();
            oProject.Resources.Save(r, 0);
        }

        public bool ProjectRunning()
        {
            return ProjectRunning(this.Project);
        }
        public bool ProjectRunning(string prj)
        {
            return false;
        }

    }
}
