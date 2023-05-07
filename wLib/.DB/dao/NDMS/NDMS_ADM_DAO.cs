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
    public class NDMS_ADM_DAO
    {
        protected LOG_T log = LOG_T.Instance;
        protected MYSQL_T mysql;

        protected string table_code = "data";

        public NDMS_ADM_DAO()
        {

        }

        public NDMS_ADM_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
        }

        public int CREATE_ADM()
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = "SHOW TABLES LIKE 'TCM_COU_DNGR_ADM'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append("CREATE TABLE `tcm_cou_dngr_adm` (");
                        sb.Append("`ADMCODE` CHAR(5) NOT NULL COMMENT '관리기관코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`CHPSNNM` VARCHAR(100) NULL DEFAULT NULL COMMENT '담당자명' COLLATE 'utf8_general_ci',");
                        sb.Append("`CHARGE_DEPT` VARCHAR(100) NULL DEFAULT NULL COMMENT '담당부서' COLLATE 'utf8_general_ci',");
                        sb.Append("`CTTPC` VARCHAR(20) NULL DEFAULT NULL COMMENT '연락처' COLLATE 'utf8_general_ci',");
                        sb.Append("`RM` VARCHAR(1000) NULL DEFAULT NULL COMMENT '비고' COLLATE 'utf8_general_ci',");
                        sb.Append("`USE_YN` CHAR(1) NULL DEFAULT NULL COMMENT '사용여부' COLLATE 'utf8_general_ci',");
                        sb.Append("`RGSDE` DATETIME NULL DEFAULT current_timestamp() COMMENT '최초등록일시',");
                        sb.Append("`UPDDE` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT '최종수정일시',");
                        sb.Append("PRIMARY KEY(`ADMCODE`) USING BTREE)");
                        sb.Append("COMMENT = '센싱정보 관리기관 정보'");
                        sb.Append("COLLATE = 'utf8_general_ci' ENGINE = InnoDB ;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.ADM): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.ADM)");
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

        public int CREATE_DSRISK()
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = "SHOW TABLES LIKE 'TCM_COU_DNGR_DSRISK'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append("CREATE TABLE `tcm_cou_dngr_dsrisk` (");
                        sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`DSNAME` VARCHAR(100) NULL DEFAULT NULL COMMENT '재해위험지구명/시설물 명' COLLATE 'utf8_general_ci',");
                        sb.Append("`DSADDR` VARCHAR(200) NULL DEFAULT NULL COMMENT '상세주소' COLLATE 'utf8_general_ci',");
                        sb.Append("`BDONG_CD` VARCHAR(10) NULL DEFAULT NULL COMMENT '법정동코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`LAT` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '위도',");
                        sb.Append("`LON` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '경도',");
                        sb.Append("`DSAPPDAY` VARCHAR(8) NULL DEFAULT NULL COMMENT '지정일자' COLLATE 'utf8_general_ci',");
                        sb.Append("`DSTYPE` VARCHAR(50) NULL DEFAULT NULL COMMENT '유형' COLLATE 'utf8_general_ci',");
                        sb.Append("`DSFACNM` VARCHAR(100) NULL DEFAULT NULL COMMENT '시설명' COLLATE 'utf8_general_ci',");
                        sb.Append("`DSRESN` VARCHAR(200) NULL DEFAULT NULL COMMENT '지정사유' COLLATE 'utf8_general_ci',");
                        sb.Append("`RELDAY` VARCHAR(8) NULL DEFAULT NULL COMMENT '해제일자' COLLATE 'utf8_general_ci',");
                        sb.Append("`RELRESN` VARCHAR(200) NULL DEFAULT NULL COMMENT '해제사유' COLLATE 'utf8_general_ci',");
                        sb.Append("`ADMCODE` CHAR(5) NULL DEFAULT NULL COMMENT '관리기관코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`RM` VARCHAR(1000) NULL DEFAULT NULL COMMENT '비고' COLLATE 'utf8_general_ci',");
                        sb.Append("`USE_YN` CHAR(1) NULL DEFAULT NULL COMMENT '사용여부' COLLATE 'utf8_general_ci',");
                        sb.Append("`RGSDE` DATETIME NULL DEFAULT current_timestamp() COMMENT '최초등록일시',");
                        sb.Append("`UPDDE` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT '최종수정일시',");
                        sb.Append("PRIMARY KEY(`DSCODE`) USING BTREE)");
                        sb.Append("COMMENT = '조기경보체계(재해위험지구) /노후·위험시설물 정보'");
                        sb.Append("COLLATE = 'utf8_general_ci' ENGINE = InnoDB ;");


                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.DSRISK): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.DSRISK)");
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

