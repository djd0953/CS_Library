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
    public class NDMS_ALERT_DAO
    {
        protected LOG_T log = LOG_T.Instance;
        protected MYSQL_T mysql;

        protected string table_code = "data";

        public NDMS_ALERT_DAO()
        {

        }

        public NDMS_ALERT_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
        }

        public int CREATE_ALERT()
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = "SHOW TABLES LIKE 'TCM_COU_DNGR_ALMORD'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append("CREATE TABLE `tcm_cou_dngr_almord` (");
                        sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`ALMCODE` CHAR(2) NOT NULL COMMENT '경보코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`ALMDE` VARCHAR(14) NOT NULL COMMENT '경보발령일시' COLLATE 'utf8_general_ci',");
                        sb.Append("`ALMGB` CHAR(1) NOT NULL COMMENT '발령구분' COLLATE 'utf8_general_ci',");
                        sb.Append("`ALMNOTE` VARCHAR(1000) NULL DEFAULT NULL COMMENT '경보발령내용' COLLATE 'utf8_general_ci',");
                        sb.Append("`ADMCODE` CHAR(5) NULL DEFAULT NULL COMMENT '관리기관코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`RGSDE` DATETIME NULL DEFAULT current_timestamp() COMMENT '최초등록일시',");
                        sb.Append("`UPDDE` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT '최종수정일시',");
                        sb.Append("PRIMARY KEY(`DSCODE`, `ALMCODE`, `ALMDE`, `ALMGB`) USING BTREE)");
                        sb.Append("COMMENT = '위험경보발령 정보'");
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

            try
            {
                sql = "SHOW TABLES LIKE 'TCM_COU_CRIT_OBSV'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append("CREATE TABLE `tcm_cou_crit_obsv` (");
                        sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`CD_DIST_OBSV` INT(4) NOT NULL COMMENT '계측기 순번',");
                        sb.Append("`GB_OBSV` CHAR(2) NULL DEFAULT NULL COMMENT '계측기 구분' COLLATE 'utf8_general_ci',");
                        sb.Append("`LEV_VALUE` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '임계치 값',");
                        sb.Append("`CRIT_LEVEL` INT(1) NOT NULL COMMENT '임계치 단계',");
                        sb.Append("PRIMARY KEY(`DSCODE`, `CD_DIST_OBSV`, `CRIT_LEVEL`) USING BTREE)");
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
    }
}

