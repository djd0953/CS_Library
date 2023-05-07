using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class CBWATER_T : CB_LOGGER_T
    {
        public CBWATER_T()
        {
            type = WB_DATA_TYPE.WATER;
        }

        public List<int> GetData_1min(DateTime datatime)
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
