using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHGATE_VO
    {
        public string Key
        {
            get
            {
                return JHGSCode;
            }
        }

        // int 11
        public string JHGSCode { get; set; } // AUTO_PK
        // int 11
        public string JHAreaCode { get; set; }
        // varchar 20
        public string JHGate { get; set; }
        // varchar 20
        public string JHLight { get; set; }
        // varchar 20
        public string JHSound { get; set; }
        // varchar 20
        public string JHStatus { get; set; }
        // varchar 20
        public string JHDate { get; set; }
    }
}
