using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHGATESTATUS_VO
    {
        public string Key
        {
            get
            {
                return JHAreaCode;
            }
        }

        // int 11
        public string JHAreaCode { get; set; } // PK
        // varchar 20
        public string JHDate { get; set; } // 0000-00-00 00:00:00
        // varchar 10
        public string JHAuto { get; set; } // ON, OFF
        // varchar 10
        public string JHGate { get; set; } // OPEN
        // varchar 10
        public string JHLightUse { get; set; } // ON, OFF
        // varchar 10
        public string JHLight { get; set; } // ON, OFF
        // varchar 10
        public string JHSoundUse { get; set; } // ON, OFF
        // varchar 10
        public string JHSound { get; set; } // ON, OFF
    }
}
