using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDSTATUS_DTMF_VO
    {
        public string Key
        {
            get
            {
                return SendCode;
            }
        }

        public string SendCode { get; set; }
        public string Cd_dist_obsv { get; set; }
        public string BrdStatus{ get; set; }
        public string AmpPower { get; set; }
        public string Speaker { get; set; }
        public string DCPower{ get; set; }
        public string RFPower{ get; set; }
        public string AmpLevel{ get; set; }
        public string DCLevel{ get; set; }
        public string RetDate { get; set; }
    }
}
