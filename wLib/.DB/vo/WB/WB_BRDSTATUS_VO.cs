using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDSTATUS_VO
    {
        public string Key
        {
            get
            {
                return Cd_dist_obsv;
            }
        }

        public string Cd_dist_obsv { get; set; } // PK
        public string Output { get; set; }
        public string Volume { get; set; }
        public string Bell { get; set; }
        public string LastSync { get; set; }
        public string Relay { get; set; }
        public string BStatus { get; set; }
        public string UDate { get; set; }
    }
}
