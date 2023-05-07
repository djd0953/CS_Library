using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDLISTDETAIL_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", BCode, Cd_dist_obsv);
            }
        }

        public string BCode { get; set; } // PK
        public string Cd_dist_obsv { get; set; } // PK
        public string BrdStatus { get; set; }
        public string ErrLog { get; set; }
        public string RegDate { get; set; }
    }
}
