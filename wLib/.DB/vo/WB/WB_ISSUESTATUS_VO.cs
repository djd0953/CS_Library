using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISSUESTATUS_VO
    {
        public string Key
        {
            get
            {
                return GCode;
            }
        }

        public string GCode { get; set; } // AUTO_PK
        public string isuCode { get; set; }
        public string issueGrade { get; set; }
        public string issueState { get; set; }
        public string Occur { get; set; }
    }
}
