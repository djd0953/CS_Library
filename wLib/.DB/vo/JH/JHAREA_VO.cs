using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHAREA_VO
    {
        public string Key
        {
            get
            {
                return JHAreaCode;
            }
        }
        
        public string Key_sub_obsv
        {
            get
            {
                return JHAreaCode + "_";
            }
        }

        public string JHAreaCode { get; set; }
        public string JHName { get; set; }
        public string JHComSys { get; set; }
        public string JHComModel { get; set; }
        public string JHPhone { get; set; }
        public string JHIP { get; set; }
        public string JHPort { get; set; }
        public string JHLastDate { get; set; }
        public string JHLastStatus { get; set; }
        public string JHSelect { get; set; }
        public string JHOnOff { get; set; }
        public string JHRainBit { get; set; }
        
        // TEMP COLUMN
        public string Data { get; set; }

    }
}
