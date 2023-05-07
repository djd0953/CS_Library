using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISULIST_VO
    {
        public string Key
        {
            get
            {
                return IsuCode;
            }
        }

        public string IsuCode { get; set; } // AUTO_PK
        public string GCode { get; set; }
        public string IsuKind { get; set; }
        public string IsuSrtAuto { get; set; }
        public string IsuSrtDate { get; set; }
        public string IsuEndAuto { get; set; }
        public string IsuEndDate { get; set; }
        public string Occur { get; set; }
        public string Equip { get; set; }
        public string SMS { get; set; }
        public string IStatus { get; set; }
        public string Send { get; set; }
        public string HAOK { get; set; }

        
    }
}
