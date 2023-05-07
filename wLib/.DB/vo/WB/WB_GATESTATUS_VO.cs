using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_GATESTATUS_VO
    {
        public string Key
        {
            get
            {
                return Cd_dist_obsv;
            }
        }

        public string Cd_dist_obsv { get; set; } // PK
        public string RegDate { get; set; }
        public string Gate { get; set; }
    }
}
