using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class NDMS_WAL_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", FlCode, Cd_dist_wal.ToString());
            }
        }

        public string FlCode { get; set; } // PK
        public string Cd_dist_wal { get; set; } // PK
        public string Nm_dist_wal { get; set; }
        public string Gb_wal { get; set; }
        public string Last_colct_de { get; set; }
        public string Last_colct_wal { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Rm { get; set; }
        public string Use_YN { get; set; }
    }
}
