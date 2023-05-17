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
    public class NDMS_ALMORD_DAO : DAO_T
    {
        protected LOG_T log = LOG_T.Instance;

        public NDMS_ALMORD_DAO()
        {

        }

        public NDMS_ALMORD_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "TCM_COU_DNGR_ALMORD";
        }

        public int Create(NDMS_ALMORD_VO vo)
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = $"SHOW TABLES LIKE '{vo.table_name}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{vo.table_name}` (");

                        if (vo.table_name == "TCM_COU_DNGR_ALMORD")
                        {
                            sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
                            sb.Append("`CD_DIST_OBSV` INT(4) NOT NULL COMMENT '계측기 순번',");
                        }
                        else if (vo.table_name == "TCM_FLUD_ALMORD")
                        {
                            sb.Append("`FLCODE` CHAR(10) NOT NULL COMMENT '침수지점코드' COLLATE 'utf8_general_ci',");
                            sb.Append("`CD_DIST_WAL` INT(4) NOT NULL COMMENT '계측기 순번',");
                        }

                        sb.Append("`ALMCODE` CHAR(2) NOT NULL COMMENT '경보코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`ALMDE` VARCHAR(14) NOT NULL COMMENT '경보발령일시' COLLATE 'utf8_general_ci',");
                        sb.Append("`ALMGB` CHAR(1) NOT NULL COMMENT '발령구분' COLLATE 'utf8_general_ci',");
                        sb.Append("`ALMNOTE` VARCHAR(1000) NULL DEFAULT NULL COMMENT '경보발령내용' COLLATE 'utf8_general_ci',");
                        sb.Append("`ADMCODE` CHAR(5) NULL DEFAULT NULL COMMENT '관리기관코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`RGSDE` DATETIME NULL DEFAULT current_timestamp() COMMENT '최초등록일시',");
                        sb.Append("`UPDDE` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT '최종수정일시',");

                        if (vo.table_name == "TCM_COU_DNGR_ALMORD") sb.Append("PRIMARY KEY(`DSCODE`, `CD_DIST_OBSV`, `ALMCODE`, `ALMDE`, `ALMGB`) USING BTREE)");
                        else if (vo.table_name == "TCM_FLUD_ALMORD") sb.Append("PRIMARY KEY(`FLCODE`, `CD_DIST_WAL`, `ALMCODE`, `ALMDE`, `ALMGB`) USING BTREE)");
                        
                        sb.Append($"COMMENT = '{vo.table_comment}'");
                        sb.Append("COLLATE = 'utf8_general_ci' ENGINE = InnoDB;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.ALMORD): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.ALMORD)");
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

        public int EndBeforeAlmord(NDMS_ALMORD_VO vo)
        {
            string sql;
            int rtv;

            Create(vo);

            try
            {
                sql = $"UPDATE {table} SET ALMGB = '2' WHERE CD_DIST_OBSV = {vo.Cd_dist_obsv}";
                rtv = mysql.ExecuteNonQuery(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int Insert(NDMS_ALMORD_VO vo)
        {
            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            Create(vo);

            try
            {
                column = vo.table_name == "TCM_COU_DNGR_ALMORD" ? "DSCODE" : "FLCODE";
                column += ",CD_DIST_OBSV, ALMCODE, ALMDE, ALMGB, ALMNOTE, ADMCODE";

                {
                    List<string> _temp_value = new List<string>
                    {
                        $"'{vo.Dscode}'",
                        $"{vo.Cd_dist_obsv}",
                        $"'{vo.AlmCode}'",
                        $"'{vo.Almde}'",
                        $"'{vo.Almgb}'",
                        $"'{vo.Almnote}'",
                        $"'{vo.Admcode}'"
                    };

                    value = string.Join(",", _temp_value);
                }

                //CREATE SQL
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} ");
                    sb.Append($"({column})");
                    sb.Append(" VALUES ");
                    sb.Append($"({value})");

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

