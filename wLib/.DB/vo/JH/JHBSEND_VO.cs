using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHBSEND_VO
    {
        public string Key
        {
            get
            {
                return JHSCode;
            }
        }

        public string JHSCode { get; set; } // AUTO
        public string SendCode { get; set; }
        public string JHACode { get; set; }
        public string JHDeCode { get; set; }
        public string RCMD { get; set; }
        public string Parm1 { get; set; }
        public string Parm2 { get; set; }
        public string Parm3 { get; set; }
        public string Parm4 { get; set; }
        public string JHDate { get; set; }
        public string JHSend { get; set; }
        public string JHResponse { get; set; }
        public string JHReturn1 { get; set; }
        public string JHReturn2 { get; set; }
        public string JHReturn3 { get; set; }
        public string JHReturn4 { get; set; }
    }
}
