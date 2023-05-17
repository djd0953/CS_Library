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
        public double[] Data { get; set; } = new double[10];
        public double[] SecondData { get; set; } = new double[10];
        public double[] Dplace_stand { get; set; }

        public double[] firstAlert { get; set; } = new double[5];
        public double[] secondAlert { get; set; } = new double[5];

        public void SetData(WB_ISUALERT_VO vo)
        {
            AltCode = vo.AltCode;
            Cd_dist_obsv = vo.Cd_dist_obsv;
            EquType = vo.EquType;
            RainTime = vo.RainTime;
            NowType = vo.NowType;
            ChkCount = vo.ChkCount;

            Use = vo.Use;
            Std = vo.Std;

            Data = vo.Data;
            SecondData = vo.SecondData;
            Dplace_stand = vo.Dplace_stand;

            firstAlert = vo.firstAlert;
            secondAlert = vo.secondAlert;
        }
    }
}
