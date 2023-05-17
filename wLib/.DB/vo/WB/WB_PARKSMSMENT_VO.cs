using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_PARKSMSMENT_VO
    {
        public string Key
        {
            get
            {
                return SMSMentCode;
            }
        }

        public string SMSMentCode { get; set; } // AUTO_PK
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
