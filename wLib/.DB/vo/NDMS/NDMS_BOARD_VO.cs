using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class NDMS_BOARD_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", FlCode, Cd_dist_board.ToString());
            }
        }

        public string FlCode { get; set; } // PK
        public string Cd_dist_board { get; set; } // PK
        public string Nm_dist_board { get; set; }
        public string Comm_sttus { get; set; }
        public string Msg_board { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Rm { get; set; }
        public string Use_YN { get; set; }
    }
}
