using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class DATA_VO : ICloneable
    {
        public DateTime datatime = new DateTime();

        public string staCode { get; set; } = "";

        private int channel_count = 0;
        public int Channel_count
        {
            get => channel_count;
            set
            {
                channel_count = value;
                Array.Resize(ref channel_data, channel_count);
            }
        }

        public string[] channel_data = new string[0];

        public DATA_VO(int _channel_count = 0)
        {
            Channel_count = _channel_count;

            channel_data = new string[Channel_count];
        }

        public object Clone()
        {
            DATA_VO obj = new DATA_VO()
            {
                datatime = datatime,
                Channel_count = Channel_count,
                channel_data = (string[])channel_data.Clone()
            };

            return obj;
        }

        public string P()
        {
            string rtv = $"{staCode} " + string.Format("{0}: CHANNEL_COUNT: {1}", datatime, Channel_count);

            Console.WriteLine(rtv);

            return rtv;
        }

        public string Print()
        {
            string rtv = $"{staCode} " + string.Format("{0}: {1}", datatime, string.Join(", ", channel_data));

            Console.WriteLine(rtv);

            return rtv;
        }
    }

    public class DATA_WATER_VO : ICloneable
    {
        public int value { get; set; }
        public int el_value { get; set; }

        public DATA_WATER_VO()
        {

        }

        public DATA_WATER_VO(int value, int el_value)
        {
            this.value = value;
            this.el_value = el_value;
        }

        public object Clone()
        {
            DATA_WATER_VO obj = new DATA_WATER_VO()
            {
                value = value,
                el_value = value
            };

            return obj;
        }

    }
}
