using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class NDMS_THOLD_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", Dscode, Cd_dist_obsv.ToString());
            }
        }

        public string Dscode { get; set; } // PK
        public string Cd_dist_obsv { get; set; } // PK
        public string AlmCode { get; set; }
        public string Gb_obsv { get; set; } // PK
        public string Obsr_gb { get; set; } // PK
        public string Thold_value { get; set; }
        public string Rm { get; set; }
        public string Use_YN { get; set; }

        public string Table_Name { get; set; }
    }
}
