using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class EWSNOW_T : EW_LOGGER_T
    {
        public EWSNOW_T()
        {
            type = WB_DATA_TYPE.SNOW;
        }

        public List<int> GetData_1min(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("53A3"); // S A3
            List<int> rtv = new List<int>();

            // SEND
            try
            {
                vo.Data = new byte[6];
                vo.Data[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                vo.Data[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                vo.Data[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                vo.Data[3] = Convert.ToByte(datatime.ToString("dd"), 16);
                vo.Data[4] = Convert.ToByte(datatime.ToString("HH"), 16);
                vo.Data[5] = Convert.ToByte(datatime.ToString("mm"), 16);

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                int offset;
                int data;

                rx_buff = new byte[18];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    offset = 13;
                    data = (rx_buff[offset + 0] << 8) | (rx_buff[offset + 1] << 0); // mm

                    rtv.Add(data);
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public List<int> GetData_10min(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("53A5"); // S A5
            List<int> rtv = new List<int>();
            
            // SEND
            try
            {
                vo.Data = new byte[6];
                vo.Data[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                vo.Data[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                vo.Data[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                vo.Data[3] = Convert.ToByte(datatime.ToString("dd"), 16);
                vo.Data[4] = Convert.ToByte(datatime.ToString("HH"), 16);
                vo.Data[5] = Convert.ToByte(datatime.ToString("mm"), 16);

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                int offset;
                int data;

                rx_buff = new byte[36];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        offset = 13 + (i * 2);
                        data = (rx_buff[offset + 0] << 8) | (rx_buff[offset + 1] << 0);

                        rtv.Add(data);
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public List<int> GetData_1hour(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("53A7");
            List<int> rtv = new List<int>();

            // SEND
            try
            {
                vo.Data = new byte[5];
                vo.Data[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                vo.Data[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                vo.Data[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                vo.Data[3] = Convert.ToByte(datatime.ToString("dd"), 16);
                vo.Data[4] = Convert.ToByte(datatime.ToString("HH"), 16);

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                int offset;
                int data;

                rx_buff = new byte[141];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    for (int m10 = 0; m10 < 6; m10++)
                    {
                        offset = 13 + (m10 * 21);

                        for (int i = 0; i < 10; i++)
                        {
                            data = (rx_buff[offset + (i * 2) + 0] << 8) | (rx_buff[offset + (i * 2) + 1] << 0);
                            
                            rtv.Add(data);
                        }
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}
