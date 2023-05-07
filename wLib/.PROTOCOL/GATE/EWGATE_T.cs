using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using wLib.DB;
using static System.Windows.Forms.AxHost;

namespace wLib.PROTOCOL
{
    public class EWGATE_T : JH_LOGGER_T, IGATE_T
    {
        public void Open()
        {
            JH_SEND_VO vo = new JH_SEND_VO("52A1");
            SetStatus(vo);
        }

        public void Close()
        {
            JH_SEND_VO vo = new JH_SEND_VO("52A3");
            SetStatus(vo);
        }

        public void SetStatus(JH_SEND_VO vo)
        {
            // byte state = A1 | A3
            //JH_SEND_VO vo = new JH_SEND_VO($"52{state:X02}");

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
                rx_buff = new byte[9];
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

        public byte GetStatus()
        {
            JH_SEND_VO vo = new JH_SEND_VO("53A1");
            byte rtv;

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
                rx_buff = new byte[31];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    rtv = rx_buff[9]; // 차단기

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