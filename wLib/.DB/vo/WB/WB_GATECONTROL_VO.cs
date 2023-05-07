using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_GATECONTROL_VO
    {
        public string Key
        {
            get
            {
                return GCtrCode;
            }
        }

        public string GCtrCode { get; set; } // PK
        public string Cd_dist_obsv { get; set; }
        public string RegDate { get; set; }
        public string Gate { get; set; }
        public string GStatus { get; set; }
    }
}
