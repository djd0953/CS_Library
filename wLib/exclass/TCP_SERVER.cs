using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public enum TCP_SERVER_STATUS { NONE = 0, ACTIVE = 1, WARNING = 2 }

    public class TCP_SERVER : IDisposable
    {
        public Socket serv_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private bool disposedValue;
        public TCP_SERVER_STATUS Status { get; set; } = TCP_SERVER_STATUS.NONE;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                    if (serv_sock.IsBound)
                        serv_sock.Close();

                    serv_sock.Dispose();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        ~TCP_SERVER()
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

        public void Bind(int port)
        {
            try
            {
                serv_sock.Bind(new IPEndPoint(IPAddress.Any, port));
                serv_sock.Listen(50);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            Status = TCP_SERVER_STATUS.NONE;

            try
            {
                if (serv_sock.IsBound)
                    serv_sock.Close();
            }
            catch
            {

            }
        }

        public void Send(Socket sock, byte[] buff, int length)
        {
            int nSend = 0;

            try
            {
                do
                {
                    IAsyncResult result = sock.BeginSend(buff, nSend, length - nSend, SocketFlags.None, null, null);
                    if (result.AsyncWaitHandle.WaitOne(3000) == false)
                    {
                        throw new SocketException(10060);
                    }

                    int rtv = sock.EndSend(result);
                    if (sock.Connected)
                    {
                        nSend += rtv;
                    }
                    else
                    {
                        throw new SocketException(10053);//?
                    }
                } while (nSend < length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Recv(Socket sock, byte[] buff, int length)
        {
            int nRead = 0;

            try
            {
                do
                {
                    IAsyncResult result = sock.BeginReceive(buff, nRead, length - nRead, SocketFlags.None, null, null);
                    if (result.AsyncWaitHandle.WaitOne(3000) == false)
                    {
                        //clnt_sock.Shutdown(SocketShutdown.Both);
                        throw new SocketException(10060);
                    }

                    int rtv = sock.EndReceive(result);
                    if (rtv == 0)
                    {
                        sock.Shutdown(SocketShutdown.Both);
                    }

                    if (sock.Connected)
                    {
                        nRead += rtv;
                    }
                    else
                    {
                        throw new SocketException(10053);//?
                    }

                } while (nRead < length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
