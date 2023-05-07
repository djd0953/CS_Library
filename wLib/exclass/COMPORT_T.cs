using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wLib
{
    public class COMPORT_T : IDisposable
    {
        SerialPort serial = new SerialPort();
        public COMPORT_INFO comport_info = new COMPORT_INFO();

        public DateTime updateTime;
        private bool disposedValue;

        public bool IsOpen
        {
            get
            {
                try
                {
                    return serial.IsOpen;
                }
                catch
                {
                    return false;
                }
            }
        }

        public int ReadTimeOut
        {
            get
            {
                return serial.ReadTimeout;
            }

            set
            {
                serial.ReadTimeout = value;
            }
        }

        public int WriteTimeout
        {
            get
            {
                return serial.WriteTimeout;
            }

            set
            {
                serial.WriteTimeout = value;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return serial.Encoding;
            }

            set
            {
                serial.Encoding = value;
            }
        }


        public COMPORT_T()
        {

        }

        public COMPORT_T(COMPORT_INFO info)
        {
            comport_info.Port_name = info.Port_name;
            comport_info.Baud_rate = info.Baud_rate;
            comport_info.Parity_bits = info.Parity_bits;
            comport_info.Data_bits = info.Data_bits;
            comport_info.Stop_bits = info.Stop_bits;
        }

        public COMPORT_T(string Port_name, int Baud_rate, int Parity_bits, int Data_bits, int Stop_bits)
        {
            comport_info.Port_name = Port_name;
            comport_info.Baud_rate = Baud_rate;
            comport_info.Parity_bits = Parity_bits;
            comport_info.Data_bits = Data_bits;
            comport_info.Stop_bits = Stop_bits;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                    if (serial.IsOpen == true)
                        serial.Close();
                }

                serial = null;
                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        ~COMPORT_T()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Open(COMPORT_INFO info)
        {
            comport_info.Port_name = info.Port_name;
            comport_info.Baud_rate = info.Baud_rate;
            comport_info.Parity_bits = info.Parity_bits;
            comport_info.Data_bits = info.Data_bits;
            comport_info.Stop_bits = info.Stop_bits;

            Open();
        }

        public void Open()
        {
            serial.PortName = comport_info.Port_name;
            serial.BaudRate = comport_info.Baud_rate;
            switch (comport_info.Parity_bits)
            {
                case 0:
                    serial.Parity = Parity.None;
                    break;
                case 1:
                    serial.Parity = Parity.Odd;
                    break;
                case 2:
                    serial.Parity = Parity.Even;
                    break;
                case 3:
                    serial.Parity = Parity.Mark;
                    break;
                case 4:
                    serial.Parity = Parity.Space;
                    break;
                default:
                    serial.Parity = Parity.None;
                    break;
            }

            serial.DataBits = comport_info.Data_bits;
            switch (comport_info.Stop_bits)
            {
                case 1:
                    serial.StopBits = StopBits.One;
                    break;
                case 2:
                    serial.StopBits = StopBits.Two;
                    break;
                case 3:
                    serial.StopBits = StopBits.OnePointFive;
                    break;
                default:
                    serial.StopBits = StopBits.One;
                    break;
            }

            try
            {
                if (serial.IsOpen == true)
                {
                    Close();
                }

                serial.Open();
            }
            catch
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {
                serial.Close();
                Thread.Sleep(10);
            }
            catch { }
        }

        public string ReadLine()
        {
            string line;

            try
            {
                line = serial.ReadLine().TrimEnd('\r', '\n');
                updateTime = DateTime.UtcNow;
            }
            catch (TimeoutException)
            {
                return null;
            }
            catch
            {
                throw;
            }

            return line;
        }

        public void Write(string text)
        {
            serial.Write(text);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            serial.Write(buffer, offset, count);
        }

        public void Write(char[] buffer, int offset, int count)
        {
            serial.Write(buffer, offset, count);
        }

        public void WriteLine(string value)
        {
            serial.WriteLine(value);
        }

        public void WriteLine(string value, string escape)
        {
            string buffer = (value + escape);
            
            serial.Write(buffer);
        }

    }
}
