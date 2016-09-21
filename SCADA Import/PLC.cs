using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLR.SCADA.DCP
{
    public class Plc
    {
        public readonly Dictionary<string, Sequence> Sequnces = new Dictionary<string, Sequence>();

        public string Tag { get; set; }
        public string DEVICE_ID { get; set; }
        public string ObjRoot { get; set; }
        public int ID { get; set; }

        public Plc(int id, string tag, string device)
        {
            ID = id;
            Tag = tag.Replace(":PLC", "");

            string t = tag;
            t = t.Replace("_", "");
            t = t.Replace(".", "");
            t = t.Replace(":PLC", "");
            ObjRoot = t;
            DEVICE_ID = device;
        }

        public void AddSequence(Sequence s)
        {
            Sequnces.Add(s.Key, s);
        }

        public Sequence AddSequence(int seqNum, string zone, int ms, string description)
        {
            Sequence s = new Sequence(this, seqNum, zone, ms, description);
            AddSequence(s);
            return s;

        }
    }
}
