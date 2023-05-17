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

        public string Dscode { get; set; } // PK
        public string Cd_dist_obsv { get; set; } // PK
        public string AlmCode { get; set; } // PK
        public string Almde { get; set; } // PK
        public string Almgb { get; set; } // PK
        public string Almnote { get; set; }
        public string Admcode 
        { 
            get
            {
                return Dscode.Substring(0, 5);
            }
        }
    }
}
