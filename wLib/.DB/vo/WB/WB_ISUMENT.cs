using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISUMENT_VO
    {
        public string Key
        {
            get
            {
                return MentCode;
            }
        }

        public string MentCode { get; set; } // AUTO_PK
        public string[] BrdMent { get; set; }
        public string[] DisMent { get; set; }
        public string[] SMSMent { get; set; }
    }
}
