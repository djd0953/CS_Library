using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Converters;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace wLib.DB
{
    public class NDMS_DATA_DAO : DAO_T
    {
        protected LOG_T log = LOG_T.Instance;

        public NDMS_DATA_DAO()
        {

        }

        public NDMS_DATA_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
        }

        public int Create(NDMS_DATA_VO vo)
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = $"SHOW TABLES LIKE '{vo.table_name}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{vo.table_name}` (");
	                    sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
	                    sb.Append("`CD_DIST_OBSV` INT(4) NOT NULL COMMENT '계측기 순번',");
	                    sb.Append("`GB_OBSV` CHAR(2) NULL DEFAULT NULL COMMENT '계측기 구분' COLLATE 'utf8_general_ci',");
	                    sb.Append("`OBSR_DTTM` VARCHAR(14) NOT NULL COMMENT '관측일시' COLLATE 'utf8_general_ci',");
	                    sb.Append("`OBSR_GB` VARCHAR(2) NOT NULL COMMENT '관측값구분' COLLATE 'utf8_general_ci',");
	                    sb.Append("`OBSR_VALUE` VARCHAR(50) NULL DEFAULT NULL COMMENT '관측값' COLLATE 'utf8_general_ci',");
	                    sb.Append("`RGSDE` DATETIME NULL DEFAULT current_timestamp() COMMENT '최초등록일시',");
	                    sb.Append("`UPDDE` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT '최종수정일시',");
	                    sb.Append("PRIMARY KEY (`DSCODE`, `CD_DIST_OBSV`, `OBSR_DTTM`, `OBSR_GB`) USING BTREE,");
                        sb.Append($"CONSTRAINT `FK_{vo.table_name}_wb_equip` FOREIGN KEY (`DSCODE`, `NDMS_DIST_OBSV`) REFERENCES `wb_equip` (`DSCODE`, `NDMS_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE");
	                    sb.Append(")");
                        sb.Append($"COMMENT = '{vo.table_comment} 센싱 계측·관측 정보'");
                        sb.Append("COLLATE = 'utf8_general_ci' ENGINE = InnoDB;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{vo.table_name}): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{vo.table_name})");
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

        public int Insert(NDMS_DATA_VO vo)
        {
            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            Create(vo);

            try
            {
                column = "DSCODE, CD_DIST_OBSV, GB_OBSV, OBSR_DTTM, OBSR_GB, OBSR_VALUE";

                {
                    List<string> _temp_value = new List<string>
                    {
                        $"'{vo.Dscode}'",
                        $"{vo.Cd_dist_obsv}",
                        $"'{vo.Gb_obsv}'",
                        $"'{vo.Obsr_dttm}'",
                        $"'{vo.Obsr_gb}'",
                        $"'{vo.Obsr_value}'"
                    };

                    value = string.Join(",", _temp_value);
                }

                //CREATE SQL
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {vo.table_name} ");
                    sb.Append($"({column})");
                    sb.Append(" VALUES ");
                    sb.Append($"({value})");
                    sb.Append(" ON DUPLICATE KEY UPDATE ");
                    sb.Append($"OBSR_VALUE = '{vo.Obsr_value}'");

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

