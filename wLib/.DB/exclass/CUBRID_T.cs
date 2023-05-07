using CUBRID.Data.CUBRIDClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    static public class CUBRID_LIB
    {
        public const string lib_name = "cascci.dll";

        static CUBRID_LIB()
        {
            FileInfo fileinfo = new FileInfo("cascci.dll");
            byte[] file_data;
            int file_size;

            try
            {
                if (Environment.Is64BitProcess)
                {
                    file_size = Properties.Resources.cascci.Length;
                    file_data = Properties.Resources.cascci;
                }
                else
                {
                    file_size = Properties.Resources.cascci.Length;
                    file_data = Properties.Resources.cascci;
                }

                if (fileinfo.Exists == false || fileinfo.Length != file_size)
                {
                    using (FileStream fs = new FileStream(lib_name, FileMode.Create))
                    {
                        fs.Write(file_data, 0, file_size);
                    }

                    File.SetAttributes(lib_name, FileAttributes.Hidden);
                }
            }
            catch (Exception ex)
            {
                LOG_T.WriteError("{0}(): {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }

    public class CUBRID_T : IDisposable
    {
        // 동적 할당
        private object lockObject = new object();
        CUBRIDConnection conn = new CUBRIDConnection();
        CUBRIDCommand cmd = new CUBRIDCommand();

        /// <summary>
        /// DB서버 IP 주소
        /// </summary>
        public string Ip { get; set; } = "";
        /// <summary>
        /// DB서버 PORT 번호
        /// </summary>
        public string Port { get; set; } = "";
        /// <summary>
        /// DB서버 database 명
        /// </summary>
        public string Dbname { get; set; } = "";
        /// <summary>
        /// DB서버 접속ID
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// DB서버 접속PW
        /// </summary>
        public string Pw { get; set; } = "";
        /// <summary>
        /// DB서버 접속 캐릭터셋
        /// 기본값 utf8
        /// </summary>
        public string Charset { get; set; } = "";
        /// <summary>
        /// DB서버 접속시간
        /// 기본값 5초
        /// </summary>
        public string Timeout { get; set; } = "5";
        /// <summary>
        /// 접속문자열 반환
        /// Server=${ip};Port=${port};Databse=${dbname};Uid=${id};Password=${pw};Charset=${charset};Connection Timeout=${timeout};
        /// </summary>
        /// 
        public string StrConn
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (Ip != "") sb.Append(string.Format("Server={0};", Ip));
                if (Port != "") sb.Append(string.Format("Port={0};", Port));
                if (Dbname != "") sb.Append(string.Format("Database={0};", Dbname));
                if (Id != "") sb.Append(string.Format("User={0};", Id));
                if (Pw != "") sb.Append(string.Format("Password={0};", Pw));
                if (Charset != "") sb.Append(string.Format("Encoding={0};", Charset));

                return sb.ToString();
            }
        }

        public bool IsOpen
        {
            get
            {
                // TODO
                // DB 연결 이후 통신 절단으로 끊어질 경우, 확인필요
                return (conn.State == System.Data.ConnectionState.Open);
            }
        }

        private bool disposedValue = false;

        public CUBRID_T()
        {
            
        }

        public CUBRID_T(DB_INFO db_info)
        {
            this.Ip = db_info.Ip;
            this.Port = db_info.Port;
            this.Dbname = db_info.Dbname;
            this.Id = db_info.Id;
            this.Pw = db_info.Pw;
            this.Charset = db_info.Charset;
            this.Timeout = db_info.Timeout;

            try
            {
                Open();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">DB서버 IP 주소</param>
        /// <param name="port"> DB서버 PORT 번호</param>
        /// <param name="dbname">DB서버 database 명</param>
        /// <param name="id">DB서버 접속ID</param>
        /// <param name="pw">DB서버 접속PW</param>
        /// <param name="charset">DB서버 접속 캐릭터셋(default:"")</param>
        /// <param name="timeout">DB서버 접속시간(default:5)</param>
        public CUBRID_T(string ip, string port, string dbname, string id, string pw, string charset = "", string timeout = "5")
        {
            this.Ip = ip;
            this.Port = port;
            this.Dbname = dbname;
            this.Id = id;
            this.Pw = pw;
            this.Charset = charset;
            this.Timeout = timeout;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue == false)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                    if (conn != null)
                        conn.Dispose();

                    if (cmd != null)
                        cmd.Dispose();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                Close();

                disposedValue = true;
            }
        }

        public void Open()
        {
            lock (lockObject)
            {
                try
                {
                    // 첫 연결
                    if (conn == null)
                    {
                        conn = new CUBRIDConnection();
                        conn.ConnectionString = StrConn;
                        conn.Open();
                    }
                    else
                    {
                        // OPEN 중이면 그대로 사용
                        if (conn.State == System.Data.ConnectionState.Open)
                        {

                        }
                        else if (conn.State == System.Data.ConnectionState.Broken)
                        {
                            // TODO 확인필요
                            // 재연결
                            conn.Open();
                        }
                        else if (conn.State == System.Data.ConnectionState.Closed)
                        {
                            // TODO 확인필요
                            // 닫기 후 재연결
                            conn.Close();
                            conn.ConnectionString = StrConn;
                            conn.Open();
                        }
                        else
                        {
                            // 현재 상태가 OPEN, Broken, Closed 중이 아니면 닫고 새로 생성
                            conn.Dispose();
                            conn = new CUBRIDConnection();
                            conn.ConnectionString = StrConn;
                            conn.Open();
                        }
                    }
                }
                catch (CUBRIDException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch
                {
                    conn = null;
                    throw;
                }
            }
        }

        public void Open(string strconn)
        {
            try
            {
                // 접속문자열 파싱
                CUBRIDConnectionStringBuilder sb = new CUBRIDConnectionStringBuilder(strconn);
                Ip = sb.Server;
                Port = sb.Port.ToString();
                Dbname = sb.Database;
                Id = sb.User;
                Pw = sb.Password;
                Charset = sb.Encoding;

                Open();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// DB 연결
        /// Exception: ArgumentException
        /// </summary>
        /// <param name="ip">DB서버 IP 주소</param>
        /// <param name="port"> DB서버 PORT 번호</param>
        /// <param name="dbname">DB서버 database 명</param>
        /// <param name="id">DB서버 접속ID</param>
        /// <param name="pw">DB서버 접속PW</param>
        /// <param name="charset">DB서버 접속 캐릭터셋(default:utf-8)</param>
        /// <param name="timeout">DB서버 접속시간(default:5)</param>
        public void Open(string ip, string port, string dbname, string id, string pw, string charset = "", string timeout = "5")
        {
            try
            {
                // 접속문자열 파싱
                this.Ip = ip;
                this.Port = port;
                this.Dbname = dbname;
                this.Id = id;
                this.Pw = pw;
                this.Charset = charset;
                this.Timeout = timeout;

                Open();
            }
            catch
            {
                throw;
            }
        }

        public void Open(DB_INFO db_info)
        {
            try
            {
                // 접속문자열 파싱
                this.Ip = db_info.Ip;
                this.Port = db_info.Port;
                this.Dbname = db_info.Dbname;
                this.Id = db_info.Id;
                this.Pw = db_info.Pw;
                this.Charset = db_info.Charset;
                this.Timeout = db_info.Timeout;

                Open();
            }
            catch
            {
                throw;
            }
        }

        public void Close()
        {
            lock (lockObject)
            {
                if (conn == null)
                    return;

                try
                {
                    conn.Close();
                }
                finally
                {
                    conn = null;
                }
            }
        }

        /// <summary>
        /// 쿼리문 조회
        /// Exception: 없음
        /// </summary>
        /// <param name="query"></param>
        /// <returns>DataTable</returns>
        public System.Data.DataTable ExecuteReader(string query)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                lock (lockObject)
                {
                    // TODO
                    // CREATE 리턴 값 확인필요
                    // 값이 없을 경우 확인필요
                    CUBRIDDataAdapter adapter = new CUBRIDDataAdapter(query, conn);
                    adapter.FillSchema(dt, System.Data.SchemaType.Source);
                    adapter.Fill(dt);
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}(): {cmd.CommandText}: {ex.Message}");
#endif
                return null;
            }

            /*
             * using (CUBRIDCommand cmd = new CUBRIDCommand(query, conn))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        rtv = reader.GetValue(0);
                    }
                }
             * */
            return dt;
        }

        /// <summary>
        /// 쿼리문 실행
        /// Exception: 없음
        /// </summary>
        /// <param name="query"></param>
        /// <returns>성공시: (Insert, Update, Delete 의 경우 반영된 행의 수)실패시: -1</returns>
        public int ExecuteNonQuery(string query)
        {
            int rtv;

            try
            {
                lock (lockObject)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    // TODO
                    // CREATE 리턴 값 확인필요
                    rtv = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}(): {cmd.CommandText}: {ex.Message}");
#endif
                return -1;
            }

            /*
             * CUBRIDCommand cmd = new CUBRIDCommand(query, conn);
             * try
             * {
             *     rtv = cmd.ExecuteNonQuery();
             * }
             */
            return rtv;
        }

        /// <summary>
        /// 단일 쿼리문 조회
        /// Exception: 없음
        /// </summary>
        /// <param name="query"></param>
        /// <returns>성공시: object, 실패시: null</returns>
        public object ExecuteScalar(string query)
        {
            object rtv;

            try
            {
                lock (lockObject)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    // TODO
                    // 리턴값 확인 필요
                    rtv = cmd.ExecuteScalar();
                    if (rtv is DBNull)
                        rtv = string.Empty;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}(): {cmd.CommandText}: {ex.Message}");
#endif
                rtv = null;
            }

            return rtv;
        }

        /// <summary>
        /// 단일 쿼리문 조회
        /// Exception: 없음
        /// </summary>
        /// <param name="query"></param>
        /// <returns>성공시: T 결과, 실패시: T 기본값</returns>
        public T ExecuteScalar<T>(string query)
        {
            T rtv;

            try
            {
                lock (lockObject)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    object temp = cmd.ExecuteScalar();
                    if (temp == null || temp is DBNull)
                        rtv = default;
                    else
                    {
                        rtv = (T)(object)(Convert.ToString(temp));
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}(): {cmd.CommandText}: {ex.Message}");
#endif
                return default;
            }

            return rtv;
        }

        /// <summary>
        /// 
        /// Exception: 없음
        /// </summary>
        /// <returns>성공시 : , 실패시: -1</returns>
        public int Commit()
        {
            int rtv;

            try
            {
                lock (lockObject)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "COMMIT;";
                    // TODO
                    // COMMIT 리턴 값 확인필요
                    rtv = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}(): {cmd.CommandText}: {ex.Message}");
#endif
                return -1;
            }

            return rtv;
        }
    }
}
