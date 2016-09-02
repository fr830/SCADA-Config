using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLR.SCADA.DCP
{
    public class Plc
    {
        public Dictionary<string, Sequence> Sequnces = new Dictionary<string, Sequence>();

        public string Description { get; set; }
        public int ID { get; set; }

        public Plc(int id, string description)
        {
            ID = id;
            Description = description;
        }


        public Sequence AddSequence(int seqNum, int ms, string description)
        {
            Sequence s = new Sequence(this, seqNum, ms, description);
            Sequnces.Add(s.Key, s);
            return s;

        }
    }
}
