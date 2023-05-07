using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class REQUESTTERMSTATUS_VO
    {

        public string termid { get; set; } // 단말 ID
        public string ischeck { get; set; } // 상태 (0:미확인 / 1:확인)
        
        public void SetData(REQUESTTERMSTATUS_VO vo)
        {
            termid = vo.termid;
            ischeck = vo.ischeck;
            
        }
    }
}
