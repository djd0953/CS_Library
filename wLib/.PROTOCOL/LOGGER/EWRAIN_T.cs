using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class EWRAIN_T : EW_LOGGER_T
    {
        public struct Statistic
        {
            public int curr_data;
            public int curr_year;
            public int curr_month;
            public int pre_month;
            public int curr_day;
            public int pre_day;
            public int curr_hour;
            public int curr_hour1;
            public int curr_hour2;
            public int curr_hour3;
            public int curr_hour6;
            public int curr_hour12;
            public int curr_hour24;
        };

        public Statistic statistic = new Statistic();

        public EWRAIN_T()
        {
            type = WB_DATA_TYPE.RAIN;
        }

        public List<int> GetData_1min(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("52A3"); // R A3
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
                    data = (rx_buff[offset + 0] << 8) | (rx_buff[offset + 1] << 0); // mm * 10

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

        public int SetData_1min(DateTime datatime, int value)
        {
            EW_SEND_VO vo = new EW_SEND_VO("52A4"); // R A4
            int rtv;

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

            //RECV
            try
            {
                rx_buff = new byte[18];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = rx_buff[13] << 8 | rx_buff[14] << 0;
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
            EW_SEND_VO vo = new EW_SEND_VO("52A5"); // R A5
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
            EW_SEND_VO vo = new EW_SEND_VO("52A7"); // R A7
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

        public void GetStatistic()
        {
            EW_SEND_VO vo = new EW_SEND_VO("52A1"); // R A1

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
                rx_buff = new byte[55];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    if (   rx_buff[13] == 0xC0 && rx_buff[16] == 0xC1 && rx_buff[19] == 0xC2 && rx_buff[22] == 0xC3
                        && rx_buff[25] == 0xC4 && rx_buff[28] == 0xC5 && rx_buff[31] == 0xC6 && rx_buff[34] == 0xC7
                        && rx_buff[37] == 0xC8 && rx_buff[40] == 0xC9 && rx_buff[43] == 0xCA && rx_buff[46] == 0xCB
                        && rx_buff[49] == 0xCC)
                    {
                        statistic.curr_data   = (ushort)(rx_buff[14] << 8 | rx_buff[15] << 0);
                        statistic.curr_year   = (ushort)(rx_buff[17] << 8 | rx_buff[18] << 0);
                        statistic.curr_month  = (ushort)(rx_buff[20] << 8 | rx_buff[21] << 0);
                        statistic.pre_month   = (ushort)(rx_buff[23] << 8 | rx_buff[24] << 0);
                        statistic.curr_day    = (ushort)(rx_buff[26] << 8 | rx_buff[27] << 0);
                        statistic.pre_day     = (ushort)(rx_buff[29] << 8 | rx_buff[30] << 0);
                        statistic.curr_hour   = (ushort)(rx_buff[32] << 8 | rx_buff[33] << 0);
                        statistic.curr_hour1  = (ushort)(rx_buff[35] << 8 | rx_buff[36] << 0);
                        statistic.curr_hour2  = (ushort)(rx_buff[38] << 8 | rx_buff[39] << 0);
                        statistic.curr_hour3  = (ushort)(rx_buff[41] << 8 | rx_buff[42] << 0);
                        statistic.curr_hour6  = (ushort)(rx_buff[44] << 8 | rx_buff[45] << 0);
                        statistic.curr_hour12 = (ushort)(rx_buff[47] << 8 | rx_buff[48] << 0);
                        statistic.curr_hour24 = (ushort)(rx_buff[50] << 8 | rx_buff[51] << 0);
                    }
                    else
                    {
                        throw new Exception("프로토콜 오류");
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
        }

        public void CalcStatistic()
        {
            EW_SEND_VO vo = new EW_SEND_VO("52A2"); // R A2

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

        public void GetStatistic_offset()
        {
            EW_SEND_VO vo = new EW_SEND_VO("52B5"); // R B5

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
                rx_buff = new byte[34];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    if (rx_buff[13] == 0xC1 && rx_buff[16] == 0xC2 && rx_buff[19] == 0xC3 && rx_buff[22] == 0xC4
                        && rx_buff[25] == 0xC5 && rx_buff[28] == 0xC6)
                    {
                        statistic.curr_year = (ushort)(rx_buff[14] << 8 | rx_buff[15] << 0);
                        statistic.curr_month = (ushort)(rx_buff[17] << 8 | rx_buff[18] << 0);
                        statistic.pre_month = (ushort)(rx_buff[20] << 8 | rx_buff[21] << 0);
                        statistic.curr_day = (ushort)(rx_buff[23] << 8 | rx_buff[24] << 0);
                        statistic.pre_day = (ushort)(rx_buff[26] << 8 | rx_buff[27] << 0);
                        statistic.curr_hour = (ushort)(rx_buff[29] << 8 | rx_buff[30] << 0);
                    }
                    else
                    {
                        throw new Exception("프로토콜 오류");
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
        }

        public void SetStatistic_offset(Statistic value)
        {
            EW_SEND_VO vo = new EW_SEND_VO("52B6"); // R B6

            // SEND
            try
            {
                vo.Data = new byte[24];
                vo.Data[0] = 0x00;
                vo.Data[1] = 0x00;
                vo.Data[2] = 0x00;
                vo.Data[3] = 0x00;
                vo.Data[4] = 0x00;
                vo.Data[5] = 0x00;
                vo.Data[6] = 0xC1;
                vo.Data[7] = (byte)(value.curr_year >> 8 & 0xFF);
                vo.Data[8] = (byte)(value.curr_year >> 0 & 0xFF);
                vo.Data[9] = 0xC2;
                vo.Data[10] = (byte)(value.curr_month >> 8 & 0xFF);
                vo.Data[11] = (byte)(value.curr_month >> 0 & 0xFF);
                vo.Data[12] = 0xC3;
                vo.Data[13] = (byte)(value.pre_month >> 8 & 0xFF);
                vo.Data[14] = (byte)(value.pre_month >> 0 & 0xFF);
                vo.Data[15] = 0xC4;
                vo.Data[16] = (byte)(value.curr_day >> 8 & 0xFF);
                vo.Data[17] = (byte)(value.curr_day >> 0 & 0xFF);
                vo.Data[18] = 0xC5;
                vo.Data[19] = (byte)(value.pre_day >> 8 & 0xFF);
                vo.Data[20] = (byte)(value.pre_day >> 0 & 0xFF);
                vo.Data[21] = 0xC6;
                vo.Data[22] = (byte)(value.curr_hour >> 8 & 0xFF);
                vo.Data[23] = (byte)(value.curr_hour >> 0 & 0xFF);

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
                rx_buff = new byte[34];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    if (rx_buff[13] == 0xC1 && rx_buff[16] == 0xC2 && rx_buff[19] == 0xC3 && rx_buff[22] == 0xC4
                        && rx_buff[25] == 0xC5 && rx_buff[28] == 0xC6)
                    {
                        statistic.curr_year = (ushort)(rx_buff[14] << 8 | rx_buff[15] << 0);
                        statistic.curr_month = (ushort)(rx_buff[17] << 8 | rx_buff[18] << 0);
                        statistic.pre_month = (ushort)(rx_buff[20] << 8 | rx_buff[21] << 0);
                        statistic.curr_day = (ushort)(rx_buff[23] << 8 | rx_buff[24] << 0);
                        statistic.pre_day = (ushort)(rx_buff[26] << 8 | rx_buff[27] << 0);
                        statistic.curr_hour = (ushort)(rx_buff[29] << 8 | rx_buff[30] << 0);
                    }
                    else
                    {
                        throw new Exception("프로토콜 오류");
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
        }

        public byte GetDigit()
        {
            EW_SEND_VO vo = new EW_SEND_VO("52B1"); // R B1
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
            EW_SEND_VO vo = new EW_SEND_VO("52B2"); // R B2
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

        public void GetADC_Config()
        {
            EW_SEND_VO vo = new EW_SEND_VO("52B3"); // R B3

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
                rx_buff = new byte[25];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    
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

        public void SetADC_Config()
        {
            EW_SEND_VO vo = new EW_SEND_VO("52B4"); // R B4

            // SEND
            try
            {
                vo.Data = new byte[9];
                vo.Data[0] = 0xC1;
                //vo.Data[1] = 
                //vo.Data[2] = 
                vo.Data[3] = 0xC2;
                //vo.Data[4] =
                //vo.Data[5] =
                vo.Data[6] = 0xC3;
                //vo.Data[7] =
                //vo.Data[8] =
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
                rx_buff = new byte[25];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    if (rx_buff[7] == 0xC1 && rx_buff[10] == 0xC2 && rx_buff[13] == 0xC3 && rx_buff[16] == 0xC4 && rx_buff[19] == 0xC5)
                    {

                    }
                    else
                    {
                        throw new Exception("프로토콜 오류");
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
        }

        public byte LineTest()
        {
            EW_SEND_VO vo = new EW_SEND_VO("52B3"); // R B3
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

        public DateTime EEP_CLEAR_DAY(DateTime datatime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("52B8"); // R B8
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
            EW_SEND_VO vo = new EW_SEND_VO("52B9"); // R B9

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
