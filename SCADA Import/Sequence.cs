using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLR.SCADA.DCP
{
    public class Sequence
    {
        public string Key { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public string Station { get; set; }
        public int MS { get; set; }
        public int SeqNum { get; set; }
        public Plc Plc { get; set; }


        public Sequence(Plc p, int seqNum, int ms, string station)
        {
            Key = p.ObjRoot + "." + seqNum.ToString("00");
            SeqNum = seqNum;
            MS = ms;
            Station = station;
            Description = $"Seq:[{seqNum.ToString("00")}] MS:{ms.ToString("00")} - {station}";
            Plc = p;
            string s = p.ObjRoot;
            s = s.Replace("_", "");
            s = s.Replace(".", "");
            s = s.Replace("Slot", "S");
            ID = s + "_STN" + seqNum.ToString("00");


        }
    }
}
