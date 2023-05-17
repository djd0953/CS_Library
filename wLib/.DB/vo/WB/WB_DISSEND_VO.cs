using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DISSEND_VO
    {
        public string Key
        {
            get
            {
                return SendCode;
            }
        }

        public string SendCode { get; set; } // AUTO PK
        public string Cd_dist_obsv { get; set; }

        public string Rcmd { get; set; }
        public string Parm1 { get; set; }
        public string Parm2 { get; set; }
        public string Parm3 { get; set; }

        public string RegDate { get; set; }
        public string BStatus { get; set; }
        public string RetDate { get; set; }
        public string RetData { get; set; }

        public void SetData(WB_DISSEND_VO vo)
        {
            SendCode = vo.SendCode;
            Cd_dist_obsv = vo.Cd_dist_obsv;

            Rcmd = vo.Rcmd;
            Parm1 = vo.Parm1;
            Parm2 = vo.Parm2;
            Parm3 = vo.Parm3;

            RegDate = vo.RegDate;
            BStatus = vo.BStatus;
            RetDate = vo.RetDate;
            RetData = vo.RetData;
        }
    }
}
