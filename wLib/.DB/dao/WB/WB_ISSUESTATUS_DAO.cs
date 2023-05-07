using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISSUESTATUS_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_ISSUESTATUS_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_issuestatus";
        }

        public void Create()
        {
            try
            {
                string sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table}` (");
                        sb.Append("`idxCode` INT(11) NOT NULL AUTO_INCREMENT COMMENT '자동 증가 유일키코드',");
                        sb.Append("`GCode` INT(11) NOT NULL COMMENT '그룹코드 기준',");
                        sb.Append("`isuCode` INT(11) NULL DEFAULT NULL COMMENT '현재 발령 중인 indexnum 업데이트',");
                        sb.Append("`issueGrade` VARCHAR(20) NULL DEFAULT NULL COMMENT 'level0,level1,level2,level3,level4' COLLATE 'utf8_general_ci',");
                        sb.Append("`issueState` VARCHAR(20) NULL DEFAULT NULL COMMENT 'advance->상향 (0에서 1일경우도 포함), retreat->하향, normal->평시' COLLATE 'utf8_general_ci',");
                        sb.Append("`Occur` VARCHAR(100) NULL DEFAULT NULL COMMENT '경보발생 종류' COLLATE 'utf8_general_ci',");
                        sb.Append("`updateDate` DATETIME NULL DEFAULT NULL COMMENT '변경 일자',");
                        sb.Append("PRIMARY KEY(`idxCode`) USING BTREE,");
                        sb.Append("INDEX `GCode` (`GCode`) USING BTREE");
                        sb.Append(")");
                        sb.Append("COLLATE = 'utf8_general_ci'");
                        sb.Append("ENGINE = InnoDB;");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    if (mysql.ExecuteNonQuery(sql) == -1)
                    {

                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})\n{{ {sql} }}");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
            }
        }

        public IEnumerable<WB_ISSUESTATUS_VO> Select(string where = "1=1", string order = "GCode", string limit = "1000")
        {
            List<WB_ISSUESTATUS_VO> list = new List<WB_ISSUESTATUS_VO>();

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
                        WB_ISSUESTATUS_VO vo = new WB_ISSUESTATUS_VO();
                        {
                            try
                            {
                                vo.GCode = Convert.ToString(row["GCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.isuCode = !string.IsNullOrEmpty(Convert.ToString(row["isuCode"])) ? Convert.ToString(row["isuCode"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.issueGrade = !string.IsNullOrEmpty(Convert.ToString(row["issueGrade"])) ? Convert.ToString(row["issueGrade"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.issueState = !string.IsNullOrEmpty(Convert.ToString(row["issueState"])) ? Convert.ToString(row["issueState"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.Occur = !string.IsNullOrEmpty(Convert.ToString(row["Occur"])) ? Convert.ToString(row["Occur"]) : null;
                            }
                            catch { }

                            list.Add(vo);
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
    }
}
