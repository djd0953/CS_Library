using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class NDMS_ALMORD_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", Dscode, Cd_dist_obsv.ToString());
            }
        }

        public string Dscode { get; set; } // 침수 외 PK
        public string Flcode // 침수 PK
        { 
            set
            {
                Dscode = value;
            }
        }

        public string Cd_dist_obsv { get; set; } // PK
        public string AlmCode { get; set; } // PK
        public string Almde { get; set; } // PK : ToString("yyyyMMddHHmmss")
        public string Almgb { get; set; } // PK
        public string Almnote { get; set; }
        public string Admcode 
        { 
            get
            {
                return Dscode.Substring(0, 5);
            }
        }

        public string table_name { get; set; } = "TCM_COU_DNGR_ALMORD";
        public string table_comment { get; set; } = "위험경보발령 정보";
    }
}
