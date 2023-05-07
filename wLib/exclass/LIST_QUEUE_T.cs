using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class LIST_QUEUE_T<T> : ConcurrentQueue<T>
    {
        // Distribute 큐
        List<ConcurrentQueue<T>> list = new List<ConcurrentQueue<T>>();

        // 내부 버퍼 사이즈
        public int recv_buff_size { get; set; } = 0;
        public int send_buff_size { get; set; } = 0;

        public LIST_QUEUE_T()
        {

        }

        public void Add(ConcurrentQueue<T> item)
        {
            // 리스트 추가
            lock (list)
            {
                list.Add(item);
            }
        }

        public void Remove(ConcurrentQueue<T> item)
        {
            // 리스트 제거
            lock (list)
            {
                list.Remove(item);
            }
        }

        public new void Enqueue(T item)
        {
            // 내부 큐 삽입
            try
            {
                base.Enqueue(item);
                while ((this.Count > recv_buff_size) && (recv_buff_size != 0))
                {
                    base.TryDequeue(out _);
                }
            }
            catch
            {
                throw;
            }

            // 리스트 큐 삽입
            try
            {
                lock (list)
                {
                    //Parallel.ForEach(list, client =>
                    foreach(var client in list)
                    {
                        try
                        {
                            client.Enqueue(item);
                            while (client.Count > send_buff_size && (send_buff_size != 0))
                            {
                                client.TryDequeue(out _);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    //);
                }
            }
            catch
            {
                throw;
            }
        }

        public new bool TryDequeue(out T val)
        {
            return base.TryDequeue(out val);
        }
    }
}
