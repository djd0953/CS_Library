using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class TERMINFOVIEW_VO
    {
        public string Key
        {
            get
            {
                return termid;
            }
        }

        public string termid { get; set; } // 단말 ID
        public string regionname { get; set; } // 지역
        public string name { get; set; } // 단말명
        public string address { get; set; } // 주소
        public string lat { get; set; } // 위도
        public string lon { get; set; } // 경도
        public string status { get; set; } // 상태 (1:연결 / 2:미연결)
        public string lastcheckdtm { get; set; } // 상태 체크 시간

        public void SetData(TERMINFOVIEW_VO vo)
        {
            termid = vo.termid;
            regionname = vo.regionname;
            name = vo.name;
            address = vo.address;
            lat = vo.lat;
            lon = vo.lon;
            status = vo.status;
            lastcheckdtm = vo.lastcheckdtm;
        }
    }
}
