using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proficy.CIMPLICITY;
using Proficy.CIMPLICITY.CimServer;


namespace JLR.SCADA.DCP
{
    public class SafetyArea
    {
        public string Zone { get; set; }
        public string SA { get; set; }
        public string Description { get; set; }
        public string ALARM_CLASS { get; set; }

        public SafetyArea(string id, string description, string almcls)
        {

            string[] spilt = id.Split('_');
            Zone = spilt[0];
            SA = spilt[1];
            Description = description;
            ALARM_CLASS = almcls;// spilt[1];
            
        }
    }
}
