using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class CBRAIN_T : CB_LOGGER_T
    {
        public CBRAIN_T()
        {
            type = WB_DATA_TYPE.RAIN;
        }

        public List<int> GetData_1min(DateTime datatime)
        {
            List<int> rtv = new List<int>();

            CB_SEND_VO vo = new CB_SEND_VO("WGD");
            int offset;
            int data;

            // SEND
            try
            {
                byte[] temp = new byte[6];
                temp[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                temp[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                temp[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                temp[3] = Convert.ToByte(datatime.ToString("dd"), 16);
                temp[4] = Convert.ToByte(datatime.ToString("HH"), 16);
                temp[5] = Convert.ToByte(datatime.ToString("mm"), 16);
                vo.Data = temp;
            }
            catch
            {
                throw;
            }

            // RECV

            try
            {

            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public List<int> GetData_10min(DateTime datatime)
        {
            List<int> rtv = new List<int>();

            CB_SEND_VO vo = new CB_SEND_VO("");
            int offset;
            int data;

            // SEND
            try
            {

            }
            catch
            {
                throw;
            }

            // RECV

            try
            {

            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public List<int> GetData_1hour(DateTime datatime)
        {
            List<int> rtv = new List<int>();

            CB_SEND_VO vo = new CB_SEND_VO("");
            int offset;
            int data;

            // SEND
            try
            {

            }
            catch
            {
                throw;
            }

            // RECV

            try
            {

            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}
