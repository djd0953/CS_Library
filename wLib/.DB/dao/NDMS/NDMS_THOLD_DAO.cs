using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace wLib.DB
{
    public class NDMS_THOLD_DAO : DAO_T
    {
        protected LOG_T log = LOG_T.Instance;

        public NDMS_THOLD_DAO()
        {

        }

        public NDMS_THOLD_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
        }

        public int Create(NDMS_THOLD_VO vo)
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = $"SHOW TABLES LIKE '{vo.Table_Name}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{vo.Table_Name}` (");
                        sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`CD_DIST_OBSV` INT(4) NOT NULL COMMENT '계측기 순번',");
                        sb.Append("`ALMCODE` CHAR(2) NOT NULL COMMENT '경보코드',");
                        sb.Append("`GB_OBSV` CHAR(2) NOT NULL COMMENT '계측기 구분',");
                        sb.Append("`OBSR_GB` CHAR(2) NOT NULL COMMENT '계측값 구분',");
                        sb.Append("`THOLD_VALUE` VARCHAR(50) NOT NULL COMMENT '임계치',");
                        sb.Append("`RM` VARCHAR(1000) NULL DEFAULT NULL COMMENT '비고',");
                        sb.Append("`USE_YN` CHAR(1) NOT NULL COMMENT '사용여부',");
                        sb.Append("PRIMARY KEY(`DSCODE`, `CD_DIST_OBSV`, `ALMCODE`, `OBSR_GB`) USING BTREE)");
                        sb.Append("COMMENT = '계측/관측 센서 임계치 기본 정보'");
                        sb.Append("COLLATE = 'utf8_general_ci' ENGINE = InnoDB;");

                        sql = sb.ToString();
                    }
                    rtv = 0;
                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.OBSV): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.OBSV)");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}): {ex.Message}");
                throw;
            }

            return rtv;
        }

        public int Insert(NDMS_THOLD_VO vo)
        {

            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            Create(vo);

            try
            {
                column = "DSCODE, CD_DIST_OBSV, ALMCODE, GB_OBSV, OBSR_GB, THOLD_VALUE, USE_YN";

                {
                    List<string> _temp_value = new List<string>
                    {
                        $"'{vo.Dscode}'",
                        $"{vo.Cd_dist_obsv}",
                        $"'{vo.AlmCode}'",
                        $"'{vo.Gb_obsv}'",
                        $"'{vo.Obsr_gb}'",
                        $"'{vo.Thold_value}'",
                        $"'{vo.Use_YN}'"
                    };

                    value = string.Join(",", _temp_value);
                }

                //CREATE SQL
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {vo.Table_Name} ");
                    sb.Append($"({column})");
                    sb.Append(" VALUES ");
                    sb.Append($"({value})");
                    sb.Append(" ON DUPLICATE KEY UPDATE ");
                    sb.Append($"THOLD_VALUE = '{vo.Thold_value}'");

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

