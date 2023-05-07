using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_SAVER
    {
        // 로그
        LOG_T log = LOG_T.Instance;
        Thread save_tid;

        // CONFIG
        DB_CONF db_conf;

        // USER DEFINED
        MYSQL_T mysql = new MYSQL_T();

        public ConcurrentQueue<WB_DATA_DTO> data_queue = new ConcurrentQueue<WB_DATA_DTO>();
        public System.Data.ConnectionState Status { get; set; }
        public bool isRunning = false;

        public WB_DATA_SAVER(DB_CONF db_conf)
        {
            this.db_conf = db_conf;

            save_tid = new Thread(new ThreadStart(save_thread));
        }

        public WB_DATA_SAVER(MYSQL_T mysql)
        {
            this.mysql = mysql;
        }

        public void Start()
        {
            isRunning = true;

            if (save_tid.IsAlive == false)
            {
                save_tid = new Thread(new ThreadStart(save_thread))
                {
                    Priority = ThreadPriority.AboveNormal,
                    IsBackground = true
                };

                try
                {
                    save_tid.Start();
                }
                catch (Exception ex)
                {
                    log.Fatal("{0}::{1}(): {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    throw ex;
                }
            }
        }

        public void Stop()
        {
            isRunning = false;

            try
            {
                save_tid.Interrupt();
            }
            catch { }
        }

        private void save_thread()
        {
            try
            {
                while (isRunning)
                {
                    db_conf.ReadConfig();
                    if (db_conf.used == false)
                    {
                        log.Debug("{0}::{1}(): db_conf[{1}].used = false", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, db_conf.section);

                        TimeSpan StopTime = TimeSpan.FromSeconds(60);
                        do
                        {
                            // 큐 클리어
                            while (data_queue.TryDequeue(out _)) ;

                            Thread.Sleep(100);
                            StopTime -= TimeSpan.FromMilliseconds(100);
                        } while (StopTime.TotalMilliseconds > 0 && isRunning == true);
                        continue;
                    }

                    log.Debug("{0}::{1}(): DB 연결을 시도합니다.({2}:{3})", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, db_conf.Ip, db_conf.Dbname);

                    // DB 연결 시도
                    try
                    {
                        mysql.Open(db_conf);
                        log.Info("DB 연결에 성공 하였습니다.({0}:{1})", db_conf.Ip, db_conf.Dbname);
                    }
                    catch (Exception ex)
                    {
                        Status = System.Data.ConnectionState.Closed;
                        log.Warning("DB 연결에 실패 하였습니다.({0}:{1}): {2}", db_conf.Ip, db_conf.Dbname, ex.Message);

                        TimeSpan StopTime = TimeSpan.FromSeconds(30);
                        do
                        {
                            // 큐 클리어
                            while (data_queue.TryDequeue(out _)) ;

                            Thread.Sleep(100);
                            StopTime -= TimeSpan.FromMilliseconds(100);
                        } while (StopTime.TotalMilliseconds > 0 && isRunning == true);
                        continue;
                    }

                    // DB 접속 성공
                    Status = System.Data.ConnectionState.Open;
                    
                    while (isRunning)
                    {
                        try
                        {
                            if (mysql.IsOpen == false)
                                break;

                            // 주 처리 로직
                            while (data_queue.TryDequeue(out WB_DATA_DTO dto) && isRunning == true)
                            {
                                Process(dto);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Warning("{0}::{1}(): {2})", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                            break;
                        }

                        // TODO 확인필요
                        TimeSpan StopTime = TimeSpan.FromMilliseconds(1000);
                        do
                        {
                            Thread.Sleep(100);
                            StopTime -= TimeSpan.FromMilliseconds(100);
                        } while (StopTime.TotalMilliseconds > 0 && isRunning == true);
                    }

                    Status = System.Data.ConnectionState.Closed;
                    log.Info("DB 연결을 종료 하였습니다.({0}:{1})", db_conf.Ip, db_conf.Dbname);

                    mysql.Close();
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadInterruptedException)
            {

            }
            catch (Exception ex)
            {
                log.Error("{0}::{1}(): {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void Process(WB_DATA_DTO dto)
        {
            try
            {
                switch (dto.Type)
                {
                    case WB_DATA_TYPE.RAIN:
                        WB_DATA_RAIN_DAO rain_dao = new WB_DATA_RAIN_DAO(mysql);
                        rain_dao.INSERT_1min(dto);
                        rain_dao.INSERT_10min(dto);
                        rain_dao.INSERT_1hour(dto);
                        break;
                    case WB_DATA_TYPE.WATER:
                        WB_DATA_WATER_DAO water_dao = new WB_DATA_WATER_DAO(mysql);
                        water_dao.INSERT_1min(dto);
                        water_dao.INSERT_10min(dto);
                        water_dao.INSERT_1hour(dto);
                        break;
                    case WB_DATA_TYPE.DPLACE:
                        WB_DATA_DPLACE_DAO dplace_dao = new WB_DATA_DPLACE_DAO(mysql);
                        dplace_dao.INSERT_1min(dto);
                        dplace_dao.INSERT_10min(dto);
                        dplace_dao.INSERT_1hour(dto);
                        break;
                    case WB_DATA_TYPE.SOIL:
                        WB_DATA_SOIL_DAO soil_dao = new WB_DATA_SOIL_DAO(mysql);
                        soil_dao.INSERT_1min(dto);
                        soil_dao.INSERT_10min(dto);
                        soil_dao.INSERT_1hour(dto);
                        break;
                    case WB_DATA_TYPE.SNOW:
                        WB_DATA_SNOW_DAO snow_dao = new WB_DATA_SNOW_DAO(mysql);
                        snow_dao.INSERT_1min(dto);
                        snow_dao.INSERT_10min(dto);
                        snow_dao.INSERT_1hour(dto);
                        break;
                    case WB_DATA_TYPE.TILT:
                        WB_DATA_TILT_DAO tilt_dao = new WB_DATA_TILT_DAO(mysql);
                        tilt_dao.INSERT_1min(dto);
                        tilt_dao.INSERT_10min(dto);
                        tilt_dao.INSERT_1hour(dto);
                        break;
                    case WB_DATA_TYPE.FLOOD:
                        WB_DATA_FLOOD_DAO flood_dao = new WB_DATA_FLOOD_DAO(mysql);
                        flood_dao.INSERT_1min(dto);
                        flood_dao.INSERT_10min(dto);
                        flood_dao.INSERT_1hour(dto);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Trace("{0}::{1}(): {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
