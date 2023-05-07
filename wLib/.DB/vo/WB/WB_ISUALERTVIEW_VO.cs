using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISUALERTVIEW_VO
    {
        public string Key
        {
            get
            {
                return AltCode;
            }
        }

        // Alert Group 내용
        public string GCode { get; set; }
        public string GName { get; set; }
        public int NowLevel { get; set; }
        public string AltDate { get; set; }

        // Alert 내용
        public string AltName { get; set; }
        public string AltCode { get; set; }

        // Temp Column
        public string NowCritical { get; set; }
        public string NextCritical { get; set; }
        public string NowData { get; set; }

        public object SetData(WB_ISUALERTVIEW_VO vo)
        {
            WB_ISUALERTVIEW_VO rtv = new WB_ISUALERTVIEW_VO()
            {
                GCode = vo.GCode,
                GName = vo.GName,
                NowLevel = vo.NowLevel,
                AltDate = vo.AltDate,

                AltName = vo.AltName,
                AltCode = vo.AltCode,

                NextCritical = vo.NextCritical,
                NowCritical = vo.NowCritical,
                NowData = vo.NowData
            };

            return rtv;
        }
    }
}
