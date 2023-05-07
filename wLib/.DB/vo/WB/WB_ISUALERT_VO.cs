using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISUALERT_VO
    {
        public string Key
        {
            get
            {
                return AltCode;
            }
        }

        public string AltCode { get; set; } // AUTO_PK
        public string Cd_dist_obsv { get; set; }
        public string EquType { get; set; }
        public string RainTime { get; set; }
        public int NowType { get; set; }
        public int ChkCount { get; set; }

        public string[] Use { get; set; } = new string[5];
        public string[] Std { get; set; } = new string[5];

        // TEMP COLUMN
        public string NowCritical {  get; set; }
        public string NextCritical { get; set; }
        public string NowData { get; set; }

        public object SetData(WB_ISUALERT_VO vo)
        {
            WB_ISUALERT_VO rtv = new WB_ISUALERT_VO()
            {
                AltCode = vo.AltCode,
                Cd_dist_obsv = vo.Cd_dist_obsv,
                EquType = vo.EquType,
                RainTime = vo.RainTime,
                NowType = vo.NowType,
                ChkCount = vo.ChkCount,

                Use = vo.Use,
                Std = vo.Std,

                NowCritical = vo.NowCritical,
                NextCritical = vo.NextCritical,
                NowData = vo.NowData
            };

            return rtv;
        }
    }
}
