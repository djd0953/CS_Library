using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class EWDP_T : EW_LOGGER_T
    {
        public EWDP_T()
        {
            type = WB_DATA_TYPE.DPLACE;
        }

        public List<int> GetData_1min(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("50A3"); // P A3
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

                rx_buff = new byte[14];
                client.Peek(rx_buff, rx_buff.Length);
                int nCount = rx_buff[13];
                Array.Resize(ref rx_buff, 14 + (nCount * 4) + 3);
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        offset = 14 + (i * 4);

                        if (rx_buff[offset] == i + 1)
                        {
                            if (rx_buff[offset + 1] == '+')
                            {
                                data = (rx_buff[offset + 2] << 8) | (rx_buff[offset + 3] << 0); // mm

                                rtv.Add(data);
                            }
                            else if (rx_buff[offset + 1] == '-')
                            {
                                data = (rx_buff[offset + 2] << 8 | (rx_buff[offset + 3] << 0)) * -1; // mm
                                rtv.Add(data);
                            }
                            else
                            {
                                throw new Exception("프로토콜 오류");
                            }
                        }
                        else
                        {
                            throw new Exception("프로토콜 오류(센서 번호 오류)");
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

        public List<int> GetData_1min(DateTime datatime, int index)
        {
            EW_SEND_VO vo = new EW_SEND_VO("50A3"); // P A3
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

                rx_buff = new byte[14];
                client.Peek(rx_buff, rx_buff.Length);
                int nCount = rx_buff[13];
                Array.Resize(ref rx_buff, 14 + nCount * 4 + 3);

                client.Recv(rx_buff);
                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        if (i != index)
                        {
                            continue;
                        }

                        offset = 14 + (i * 4);

                        if (rx_buff[offset] == i + 1)
                        {
                            if (rx_buff[offset + 1] == '+')
                            {
                                data = rx_buff[offset + 2] << 8 | rx_buff[offset + 3] << 0;
                                rtv.Add(data);
                            }
                            else if (rx_buff[offset + 1] == '-')
                            {
                                data = (rx_buff[offset + 2] << 8 | rx_buff[offset + 3] << 0) * -1;
                                rtv.Add(data);
                            }
                            else
                            {
                                throw new Exception("프로토콜 오류");
                            }
                        }
                        else
                        {
                            throw new Exception("프로토콜 오류(센서 번호 오류)");
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

        public int SetData_1min(DateTime datatime, int index, int value)
        {
            EW_SEND_VO vo = new EW_SEND_VO("50A4"); // P A4
            int rtv;

            // SEND
            try
            {
                vo.Data = new byte[9];
                vo.Data[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                vo.Data[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                vo.Data[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                vo.Data[3] = Convert.ToByte(datatime.ToString("dd"), 16);
                vo.Data[4] = Convert.ToByte(datatime.ToString("HH"), 16);
                vo.Data[5] = Convert.ToByte(datatime.ToString("mm"), 16);
                vo.Data[6] = (byte)index;
                vo.Data[7] = (byte)(value >> 8 & 0xFF);
                vo.Data[8] = (byte)(value >> 0 & 0xFF);

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            //RECV
            try
            {
                rx_buff = new byte[19];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = rx_buff[14] << 8 | rx_buff[15] << 0;
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

        public List<int> GetData_10min(DateTime datatime, int index)
        {
            EW_SEND_VO vo = new EW_SEND_VO("50A5"); // P A5
            List<int> rtv = new List<int>();

            // SEND
            try
            {
                vo.Data = new byte[7];
                vo.Data[0] = Convert.ToByte(datatime.ToString("yyyy").Substring(0, 2), 16);
                vo.Data[1] = Convert.ToByte(datatime.ToString("yyyy").Substring(2, 2), 16);
                vo.Data[2] = Convert.ToByte(datatime.ToString("MM"), 16);
                vo.Data[3] = Convert.ToByte(datatime.ToString("dd"), 16);
                vo.Data[4] = Convert.ToByte(datatime.ToString("HH"), 16);
                vo.Data[5] = Convert.ToByte(datatime.ToString("mm"), 16);
                vo.Data[6] = Convert.ToByte(index);

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            //RECV
            try
            {
                int offset;
                int data;

                rx_buff = new byte[49];
                client.Recv(rx_buff, rx_buff.Length);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        offset = 16 + (i * 3);

                        if (rx_buff[offset + 0] == '+')
                        {
                            data = rx_buff[offset + 1] << 8 | rx_buff[offset + 2] << 0;
                            rtv.Add(data);
                        }
                        else if (rx_buff[offset + 0] == '-')
                        {
                            data = (rx_buff[offset + 1] << 8 | rx_buff[offset + 2] << 0) * -1;
                            rtv.Add(data);
                        }
                        else
                        {
                            throw new Exception("프로토콜 오류");
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

        public List<int> GetData_1hour(DateTime datatime, int index)
        {
            EW_SEND_VO vo = new EW_SEND_VO("50A7"); // P A7
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
                vo.Data[5] = Convert.ToByte(index);

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

                rx_buff = new byte[204];
                client.Recv(rx_buff, rx_buff.Length);
                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    for (int m10 = 0; m10 < 6; m10++)
                    {
                        offset = 16 + (m10 * 31);

                        for (int i = 0; i < 10; i++)
                        {
                            if (rx_buff[offset + 0] == '+')
                            {
                                data = rx_buff[offset + (i * 3) + 1] << 8 | rx_buff[offset + (i * 3) + 2] << 0;
                                rtv.Add(data);
                            }
                            else if (rx_buff[offset + 0] == '-')
                            {
                                data = (rx_buff[offset + (i * 3) + 1] << 8 | rx_buff[offset + (i * 3) + 2] << 0) * -1;
                                rtv.Add(data);
                            }
                            else
                            {
                                throw new Exception("프로토콜 오류");
                            }
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

        public int GetSensorCount()
        {
            EW_SEND_VO vo = new EW_SEND_VO("50B3"); // P B3
            int rtv;

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
                rx_buff = new byte[12];
                client.Recv(rx_buff, rx_buff.Length);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = rx_buff[8];
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

        public int SetSensorCount(int value)
        {
            EW_SEND_VO vo = new EW_SEND_VO("50B4"); // P B4
            int rtv;

            // SEND
            try
            {
                vo.Data = new byte[2];
                vo.Data[0] = 0xC1;
                vo.Data[1] = (byte)value;
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
                rx_buff = new byte[12];
                client.Recv(rx_buff, rx_buff.Length);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = rx_buff[8];
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
            EW_SEND_VO vo = new EW_SEND_VO("50B8"); // P B8
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
            EW_SEND_VO vo = new EW_SEND_VO("50B9"); // P B9

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
