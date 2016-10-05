using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proficy.CIMPLICITY;
using Proficy.CIMPLICITY.CimServer;


namespace JLR.SCADA.DCP
{
    public class MachineSection
    {
        public string Zone { get; set; }
        public string MS { get; set; }
        public string Description { get; set; }
        public string ALARM_CLASS { get; set; }

        public MachineSection(string id, string description)
        {

            string[] spilt = id.Split('_');
            Zone = spilt[0];
            MS = spilt[1];
            Description = description;
            ALARM_CLASS = spilt[1];
            
        }
    }
}
