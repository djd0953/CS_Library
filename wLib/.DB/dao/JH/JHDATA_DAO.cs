using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHDATA_DAO
    {
        protected LOG_T log = LOG_T.Instance;
        protected MYSQL_T mysql;

        protected string table_code = "data";

        public JHDATA_DAO()
        {

        }

        public JHDATA_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
        }

        public void CREATE(WB_DATA_DTO dto)
        {
            string table = $"jh{table_code}";
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
                        sb.Append("(");
                        sb.Append("`JHAreaCode` INT(11) NOT NULL, ");
                        sb.Append("`JHDate` VARCHAR(10) NOT NULL, ");
                        sb.Append("`JHHour1` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour2` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour3` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour4` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour5` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour6` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour7` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour8` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour9` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour10` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour11` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour12` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour13` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour14` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour15` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour16` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour17` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour18` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour19` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour20` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour21` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour22` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour23` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHHour24` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHFree1` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHFree2` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`JHFree3` DOUBLE NULL DEFAULT '0', ");
                        sb.Append("`dtmCreate` DATETIME DEFAULT current_timestamp(), ");
                        sb.Append("`dtmUpdate` DATETIME DEFAULT NULL ON UPDATE current_timestamp(), ");
                        sb.Append("PRIMARY KEY(`JHAreaCode`, `JHDate`), ");
                        sb.Append($"CONSTRAINT `FK_{table}` FOREIGN KEY (`JHAreaCode`) REFERENCES `jharea` (`JHAreaCode`) ON UPDATE CASCADE ON DELETE NO ACTION");
                        sb.Append(") ENGINE = InnoDB COLLATE = 'utf8_general_ci';");
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

        public string[] SELECT(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string JHAreaCode, JHDate;
            // SQL
            string sql;

            string[] rtv = new string[24];

            try
            {
                // CREATE SQL
                {
                    table = $"jh{table_code}";
                    JHAreaCode = dto.Cd_dist_obsv;
                    JHDate = $"{dto.Datatime:yyyyMMdd}";

                    // SQL
                    sql = $"SELECT JHHour1, JHHour2, JHHour3, JHHour4, JHHour5, JHHour6, JHHour7, JHHour8, JHHour9, JHHour10, JHHour11, JHHour12, JHHour13, JHHour14, JHHour15, JHHour16, JHHour17, JHHour18, JHHour19, JHHour20, JHHour21, JHHour22, JHHour23, JHHour24 " +
                          $"FROM {table} " +
                          $"WHERE JHAreaCode = '{JHAreaCode}' AND JHDate = '{JHDate}' ";
                }

                object result = mysql.ExecuteReader(sql);
                if (result == null || result == DBNull.Value)
                {
                    // 없을 경우
                }
                else
                {
                    // 있을 경우
                    System.Data.DataTable dt = result as System.Data.DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rtv = dt.Select().First().ItemArray.Select(x => Convert.ToString(x)).ToArray();
                    }
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int INSERT(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string JHAreaCode, JHDate;
            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            CREATE(dto);

            try
            {
                table = $"jh{table_code}";
                JHAreaCode = dto.Cd_dist_obsv;
                JHDate = $"{dto.Datatime:yyyyMMdd}";
                column = $"JHHour{dto.Datatime.Hour + 1}";
                value = dto.Value;

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} (JHAreaCode, JHDate, {column}) ");
                    sb.Append($"VALUES('{JHAreaCode}', '{JHDate}', {value}) ");
                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"{column} = {value} ");

                    sql = sb.ToString();
                }

                rtv = mysql.ExecuteNonQuery(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}
