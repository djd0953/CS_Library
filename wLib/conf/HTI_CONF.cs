using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class HTI_CONF
    {
        CONFIG config = new CONFIG("HTI.ini");

        // 설정파일 항목([섹션])
        private string section;

        // 설정파일 항목(키=기본값)
        public string outputDir;

    }
}
