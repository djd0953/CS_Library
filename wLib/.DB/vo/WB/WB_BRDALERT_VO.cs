using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDALERT_VO
    {
        public string Key
        {
            get
            {
                return AltCode;
            }
        }

        public string AltCode { get; set; } // AUTO PK
        public string Title { get; set; }
        public string Content { get; set; }

        public void SetData(WB_BRDALERT_VO vo)
        {
            AltCode = vo.AltCode;
            Title = vo.Title;
            Content = vo.Content;
        }
    }
}
