using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDLIST_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}", BCode);
            }
        }

        public string BCode { get; set; } // PK
        public string Cd_dist_obsv { get; set; } // delim ,
        public string Title { get; set; }
        public string BType { get; set; }
        public string BrdType { get; set; }
        public string AltMent { get; set; }
        public string TTSContent { get; set; }
        public string RevType { get; set; }
        public string BrdDate { get; set; }
        public string BRepeat { get; set; }
        public string RegDate { get; set; }
        public string IsuCode { get; set; }
    }
}
