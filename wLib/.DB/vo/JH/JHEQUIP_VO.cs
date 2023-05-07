using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHEQUIP_VO
    {
        public string Key
        {
            get => JHECode;
        }

        /// <summary>
        /// AUTO
        /// </summary>
        public string JHECode { get; set; }
        public string JHACode { get; set; }
        public string JHDeCode { get; set; }
        public string JHName { get; set; }
        public string JHPhone { get; set; }
    }
}
