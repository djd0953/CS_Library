using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDCID_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_BRDCID_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_brdcid";
        }

        // TODO 
        // 미확인
        public void Create()
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table}` ");
                        sb.Append("(");
                        sb.Append(" `CidCode` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'AUTO_PK', ");
                        sb.Append(" `CD_DIST_OBSV` VARCHAR(10) NULL DEFAULT NULL, ");
                        sb.Append("	`Cid` VARCHAR(20) NULL DEFAULT NULL, ");
                        sb.Append("	`CStatus` VARCHAR(10) NULL DEFAULT NULL, ");
                        sb.Append("	`RegDate` VARCHAR(20) NULL DEFAULT NULL, ");
                        sb.Append("	`RetDate` DATETIME NULL DEFAULT NULL, ");
                        sb.Append("PRIMARY KEY (`CidCode`), ");
                        sb.Append($"INDEX `IDX_{table}` (`CD_DIST_OBSV`), ");
                        sb.Append($"CONSTRAINT `FK_{table}` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE NO ACTION ");
                        sb.Append(") ENGINE = InnoDB DEFAULT CHARSET = utf8; ");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);

                    if (rtv == -1)
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {sql}");
                    else log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 에러({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
                throw;
            }
        }

        public IEnumerable<WB_BRDCID_VO> Select(string where = "1=1", string order = "CidCode", string limit = "1000")
        {
            List<WB_BRDCID_VO> list = new List<WB_BRDCID_VO>();

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
                        try
                        {
                            WB_BRDCID_VO vo = new WB_BRDCID_VO();
                            {
                                try
                                {
                                    vo.CidCode = Convert.ToString(row["CidCode"]);

                                }
                                catch { }

                                try
                                {
                                    vo.Cd_dist_obsv = Convert.ToString(row["Cd_dist_obsv"]);
                                }
                                catch { }

                                try
                                {
                                    vo.Cid = Convert.ToString(row["Cid"]);

                                }
                                catch { }

                                try
                                {
                                    vo.CStatus = Convert.ToString(row["CStatus"]);
                                }
                                catch { }

                                try
                                {
                                    vo.RegDate = Convert.ToString(row["RegDate"]);
                                }
                                catch { }
                            };

                            list.Add(vo);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        public int Insert(WB_BRDCID_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(Cd_dist_obsv, Cid, CStatus, RegDate) " +
                      $"VALUES('{vo.Cd_dist_obsv}', '{vo.Cid}', '{vo.CStatus}', '{vo.RegDate}') ";

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
