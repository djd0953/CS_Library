using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDLISTDETAIL_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_BRDLISTDETAIL_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_brdlistdetail";
        }

        public void Create()
        {
            string sql;

            try
            {
                sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table}` ");
                        sb.Append("( ");
                        sb.Append(@"`BCode` INT(11) NOT NULL COMMENT 'wb_brdlist.BCode', `CD_DIST_OBSV` VARCHAR(10) NULL DEFAULT NULL COMMENT 'wb_brdlist.CD_DIST_OBSV', `BrdStatus` VARCHAR(10) NULL DEFAULT NULL COMMENT '상태(""start, OK, Fail"")', `ErrLog` VARCHAR(50) NULL DEFAULT NULL COMMENT 'BrdStatus 가 Fail 인 사유', `RegDate` VARCHAR(20) NULL DEFAULT NULL COMMENT '등록시간', `RetDate` DATETIME NULL DEFAULT NULL COMMENT '응답시간', INDEX `BCode` (`BCode`) USING BTREE");
                        sb.Append(")");
                        sb.Append("COLLATE='utf8_general_ci' ");
                        sb.Append("ENGINE=InnoDB; ");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    mysql.ExecuteNonQuery(sql);

                    log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
                throw;
            }
        }

        public IEnumerable<WB_BRDLISTDETAIL_VO> Select(string where = "1=1", string order = "BCode, CD_DIST_OBSV", string limit = "1000")
        {
            List<WB_BRDLISTDETAIL_VO> list = new List<WB_BRDLISTDETAIL_VO>();

            try
            {
                base.where = where;
                base.order = order;
                base.limit = limit;

                DataTable dt = base.Select();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        WB_BRDLISTDETAIL_VO vo = new WB_BRDLISTDETAIL_VO();
                        {
                            try
                            {
                                vo.BCode = Convert.ToString(row["BCode"]);
                            }
                            catch { }

                            try
                            {
                                vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.ErrLog = Convert.ToString(row["ErrLog"]);
                            }
                            catch { }

                            try
                            {
                                vo.BrdStatus = Convert.ToString(row["BrdStatus"]);
                            }
                            catch { }
                        }

                        list.Add(vo);
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        public int Insert(WB_BRDLISTDETAIL_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}(BCode, CD_DIST_OBSV, ErrLog, BrdStatus) " +
                      $"VALUES('{vo.BCode}', '{vo.Cd_dist_obsv}', '{vo.ErrLog}', '{vo.BrdStatus}') " +
                      $"ON DUPLICATE KEY UPDATE " +
                      $"BCode = '{vo.BCode}', " +
                      $"Cd_dist_obsv = '{vo.Cd_dist_obsv}', " +
                      $"ErrLog = '{vo.ErrLog}', " +
                      $"BrdStatus = '{vo.BrdStatus}' ";

                rtv = base.Insert(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}
