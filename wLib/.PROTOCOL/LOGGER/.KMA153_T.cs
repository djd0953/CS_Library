using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class KMA153_T
    {
        public const int MAX_LENGTH = 156;
        private byte[] Raw = new byte[0];

        private byte[] Header = new byte[2];
        private byte[] Version = new byte[3];
        private byte[] Command = new byte[2];
        private byte[] Data = new byte[0];
        private byte[] Crc = new byte[1];
        private byte[] Footer = new byte[2];


    }
}
