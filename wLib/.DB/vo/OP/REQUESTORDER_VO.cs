using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class REQUESTORDER_VO
    {
        public string Key
        {
            get
            {
                return requestorderId;
            }
        }

        public string requestorderId { get; set; } // 방송 요청 ID
        public string mode { get; set; } // 모드 (1:실제 / 2:시험)
        public string mediatype { get; set; } // 방송매체 (2:TCP / 3:SMS / 4:VMS)
        public string disastermsg { get; set; } // 방송내용
        public string listtermid { get; set; } // 방송대상리스트
        

        public void SetData(REQUESTORDER_VO vo)
        {
            requestorderId = vo.requestorderId;
            mode = vo.mode;
            mediatype = vo.mediatype;
            disastermsg = vo.disastermsg;
            listtermid = vo.listtermid;
            
        }
    }
}
