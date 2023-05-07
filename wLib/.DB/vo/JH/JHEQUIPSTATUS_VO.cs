using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHEQUIPSTATUS_VO
    {
        public string Key
        {
            get
            {
                return JHSCode;
            }
        }

        public string JHSCode { get; set; }
        public string JHACode { get; set; }
        public string JHDeCode { get; set; }
        public string Volume { get; set; }
        public string Output { get; set; }
        public string Relay { get; set; }
        public string Bell { get; set; }
        public string PlayTime { get; set; }
        public string JHDate { get; set; }
        public string SendOK { get; set; }
    }
}
