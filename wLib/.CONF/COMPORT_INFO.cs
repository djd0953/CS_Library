using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class COMPORT_INFO
    {
        public virtual string Port_name { get; set; } = "COM1";
        public virtual int Baud_rate { get; set; } = 9600;
        public virtual int Parity_bits { get; set; } = 0;
        public virtual int Data_bits { get; set; } = 8;
        public virtual int Stop_bits { get; set; } = 1;
    }
}
