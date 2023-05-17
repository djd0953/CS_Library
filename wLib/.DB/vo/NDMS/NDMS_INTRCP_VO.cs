using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class NDMS_INTRCP_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", FlCode, Cd_dist_intrcp.ToString());
            }
        }

        public string FlCode { get; set; } // PK
        public string Cd_dist_intrcp { get; set; } // PK
        public string Nm_dist_intrcp { get; set; }
        public string Gb_intrcp { get; set; }
        public string mod_intrcp { get; set; }
        public string Comm_sttus { get; set; }
        public string intrcp_sttus { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Rm { get; set; }
        public string Use_YN { get; set; }
    }
}
