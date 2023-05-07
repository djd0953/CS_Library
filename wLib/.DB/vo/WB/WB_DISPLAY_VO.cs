using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DISPLAY_VO
    {
        public string Key
        {
            get
            {
                return DisCode;
            }
        }

        public string DisCode { get; set; } // AUTO_PK
        public string CD_DIST_OBSV { get; set; }
        public string SaveType { get; set; }
        public string DisEffect { get; set; }
        public string DisSpeed { get; set; }
        public string DisTime { get; set; }
        public string EndEffect { get; set; }
        public string EndSpeed { get; set; }
        public string StrTime { get; set; }
        public string EndTime { get; set; }
        public string Relay { get; set; }
        public string ViewImg { get; set; }
        public string SendImg { get; set; }
        public string HtmlData { get; set; }
        public string ViewOrder { get; set; }
        public string DisType { get; set; }
        public string Exp_YN { get; set; }
        public string RegDate { get; set; }
    }
}
