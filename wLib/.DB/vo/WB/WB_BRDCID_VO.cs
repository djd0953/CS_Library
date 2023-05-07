using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDCID_VO
    {
        public string Key
        {
            get
            {
                return CidCode;
            }
        }

        public string CidCode { get; set; } // AUTO
        public string Cd_dist_obsv { get; set; }
        public string Cid { get; set; }
        public string CStatus { get; set; }
        public string RegDate { get; set; }
    }
}
