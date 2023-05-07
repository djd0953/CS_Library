using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_SMSUSER_VO
    {
        public string Key
        {
            get
            {
                return GCode;
            }
        }

        public string GCode { get; set; } // AUTO_PK
        public string UName { get; set; }
        public string Division { get; set; }
        public string UPosition { get; set; }
        public string Phone { get; set; }
    }
}
