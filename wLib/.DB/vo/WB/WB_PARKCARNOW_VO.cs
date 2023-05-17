using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_PARKCARNOW_VO
    {
        public int Key
        {
            get
            {
                return idx;
            }
        }

        public int idx { get; set; } // AUTO_PK
        public string GateDate { get; set; }
        public string GateSerial { get; set; }
        public string CarNum { get; set; }
    }
}
