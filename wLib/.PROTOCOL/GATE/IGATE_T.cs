using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.PROTOCOL
{
    public enum GATE_STATUS { UNKNOWN, OPEN, CLOSE, UPLOCK, DOWNLOCK }

    interface IGATE_T
    {
        void Open();
        void Close();
        //void Lock();
    }
}
