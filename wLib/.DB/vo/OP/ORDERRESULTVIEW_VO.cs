using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class ORDERRESULTVIEW_VO
    {
        public string Key
        {
            get
            {
                return ordernum;
            }
        }

        public string ordernum { get; set; } // 방송 번호
        public string termid { get; set; } // 단말 ID

        /// <summary>
        /// 0: 발령시작
        /// 1: 실제 중 시험 무시
        /// 2: 자체(비상/지진옥내/화재옥내) 방송 중 발령 무시
        /// 3: 동일 발령 무시
        /// 4: 무응답
        /// </summary>
        public string respoonse { get; set; } // 응답 결과

        /// <summary>
        /// 0: 무응답
        /// 1: 정상울림
        /// 2: 미울림
        /// 5: 잘못울림
        /// 13: ARS
        /// 20: Time Out 자동해제
        /// 30: 단말 지역방송
        /// 31: RCC
        /// 80: 지진옥내 방송
        /// 81: 화재옥내 방송
        /// 99: 비상 방송
        /// 999: 미대상
        /// </summary>
        public string result { get; set; } // 결과

        public string requestorderId { get; set; } // 방송요청 ID

        

        public void SetData(ORDERRESULTVIEW_VO vo)
        {
            ordernum = vo.ordernum;
            termid = vo.termid;
            respoonse = vo.respoonse;
            result = vo.result;
            requestorderId = vo.requestorderId;
        }
    }
}
