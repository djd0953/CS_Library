using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class EWWATER_T : EW_LOGGER_T
    {
        public EWWATER_T()
        {
            type = WB_DATA_TYPE.WATER;
        }

        public List<DATA_WATER_VO> GetData_1min(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("57A3"); // W A3
            List<DATA_WATER_VO> rtv = new List<DATA_WATER_VO>();

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
                int data, el_data;

                rx_buff = new byte[23];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    // 관측 값
                    offset = 14;
                    data = (rx_buff[offset + 0] << 8) | (rx_buff[offset + 1] << 0); // mm

                    // E.L 값
                    offset = 17;
                    el_data = (rx_buff[offset + 0] << 16) | (rx_buff[offset + 1] << 8) | (rx_buff[offset + 2] << 0); // mm

                    rtv.Add(new DATA_WATER_VO(data, el_data));
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

        public List<DATA_WATER_VO> SetData_1min(DateTime datatime, int value)
        {
            EW_SEND_VO vo = new EW_SEND_VO("57A4"); // W A4
            List<DATA_WATER_VO> rtv = new List<DATA_WATER_VO>();

            // SEND
            try
            {
                vo.Data = new byte[8];
                vo.Data[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                vo.Data[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                vo.Data[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                vo.Data[3] = Convert.ToByte(datatime.ToString("dd"), 16);
                vo.Data[4] = Convert.ToByte(datatime.ToString("HH"), 16);
                vo.Data[5] = Convert.ToByte(datatime.ToString("mm"), 16);
                vo.Data[6] = (byte)(value >> 8 & 0xFF);
                vo.Data[7] = (byte)(value >> 0 & 0xFF);

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
                int data, el_data;

                rx_buff = new byte[23];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    // 관측 값
                    offset = 14;
                    data = (rx_buff[offset + 0] << 8) | (rx_buff[offset + 1] << 0); // mm

                    // E.L 값
                    offset = 17;
                    el_data = (rx_buff[offset + 0] << 16) | (rx_buff[offset + 1] << 8) | (rx_buff[offset + 2] << 0); // mm

                    rtv.Add(new DATA_WATER_VO(data, el_data));
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

        public List<DATA_WATER_VO> GetData_10min(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("57A5"); // W A5
            List<DATA_WATER_VO> rtv = new List<DATA_WATER_VO>();

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
                int data, el_data;

                rx_buff = new byte[40];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    // EL 값
                    offset = 34;
                    el_data = (rx_buff[offset + 0] << 16) | (rx_buff[offset + 1] << 8) | (rx_buff[offset + 2] << 0);

                    for (int i = 0; i < 10; i++)
                    {
                        // 관측 값
                        offset = 13 + (i * 2);
                        data = (rx_buff[offset + 0] << 8) | (rx_buff[offset + 1] << 0);

                        rtv.Add(new DATA_WATER_VO(data, el_data));
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

        public List<DATA_WATER_VO> GetData_1hour(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("57A7"); // W A7
            List<DATA_WATER_VO> rtv = new List<DATA_WATER_VO>();

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
                int data, el_data;

                rx_buff = new byte[145];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    // TODO E.L 값
                    offset = 139;
                    el_data = rx_buff[offset + 0] << 16 | rx_buff[offset + 1] << 8 | rx_buff[offset + 2] << 0;

                    for (int m10 = 0; m10 < 6; m10 ++)
                    {
                        offset = 13 + (m10 * 21);

                        for(int i = 0; i < 10; i++)
                        {
                            // 관측 값
                            data = (rx_buff[offset + (i * 2) + 0] << 8) | (rx_buff[offset + (i * 2) + 1] << 0);

                            rtv.Add(new DATA_WATER_VO(data, el_data));
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

        public byte GetDigit()
        {
            EW_SEND_VO vo = new EW_SEND_VO("50B1"); // P B1
            byte rtv;

            // SEND
            try
            {
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
                rx_buff = new byte[11];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = rx_buff[7];
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

        public byte SetDigit(byte value)
        {
            EW_SEND_VO vo = new EW_SEND_VO("50B2"); // P B2
            byte rtv;

            // SEND
            try
            {
                vo.Data = new byte[1];
                vo.Data[0] = value;

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
                rx_buff = new byte[11];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = rx_buff[7];
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

        public DateTime EEP_CLEAR_DAY(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("57B8"); // W B8
            DateTime rtv;

            // SEND
            try
            {
                vo.Data = new byte[4];
                vo.Data[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                vo.Data[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                vo.Data[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                vo.Data[3] = Convert.ToByte(datatime.ToString("dd"), 16);

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
                rx_buff = new byte[14];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = Convert.ToDateTime($"{rx_buff[7]:X2}{rx_buff[8]:X2}-{rx_buff[9]:X2}-{rx_buff[10]:X2}");
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

        public void EEP_CLEAR_ALL()
        {
            EW_SEND_VO vo = new EW_SEND_VO("57B9"); // W B9

            // SEND
            try
            {
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
                rx_buff = new byte[10];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    ;
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
        }
    }
}
