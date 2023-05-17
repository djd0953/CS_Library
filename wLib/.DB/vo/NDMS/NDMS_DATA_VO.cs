using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public enum NDMS_DATA_TYPE { NONE = 0, RAIN = 1, WATER = 2, DEPLACE = 3, SOIL = 4, SNOW = 6, SLOPE = 8 }

    public class NDMS_DATA_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", Dscode, Cd_dist_obsv.ToString());
            }
        }

        public string Dscode { get; set; } // PK
        public int Cd_dist_obsv { get; set; } // PK
        public string Gb_obsv { get; set; }
        public string Obsr_dttm { get; set; } // PK
        public string Obsr_gb { get; set; } // PK
        public string Obsr_value { get; set; }
        public string table_name { get; set; } = "TCM_COU_DD_OBSV";
        public string table_comment { get; set; } = "재해위험개선지구";
    }
}
