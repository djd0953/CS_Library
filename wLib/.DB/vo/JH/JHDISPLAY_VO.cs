using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHDISPLAY_VO
    {
        public string Key
        {
            get
            {
                return JHMCode;
            }
        }

        public string JHMCode{ get; set; }
        public string JHDGroup { get; set; }
        public string JHAreaCode { get; set; }
        public string JHStartDate { get; set; }
        public string JHEndDate { get; set; }
        public string JHContent { get; set; }
        public string JHHexContent { get; set; }
        public string JHEmgNum { get; set; }
        public string JHSENDOK { get; set; }
        public string JHDisOrder { get; set; }
        public string JHDate{ get; set; }
    }
}
