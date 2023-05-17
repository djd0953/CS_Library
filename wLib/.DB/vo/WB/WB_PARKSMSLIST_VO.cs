using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_PARKSMSLIST_VO
    {
        public int Key
        {
            get
            {
                return idx;
            }
        }

        public int idx { get; set; } // AUTO_PK
        public string CarNum { get; set; }
        public string CarPhone { get; set; }
        public string SMSContent { get; set; }
        public string RegDate { get; set; }
        public string EndDate { get; set; }
        public string SendStatus { get; set; }
        public string SendType { get; set; }
    }
}
