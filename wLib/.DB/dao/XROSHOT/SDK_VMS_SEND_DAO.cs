using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class SDK_VMS_SEND_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public SDK_VMS_SEND_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "sdk_vms_send";
        }

        public int LastInsertID()
        {
            int rtv = -1;

            try
            {
                DataTable dt = base.Select($"SELECT LAST_INSERT_ID()");
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        rtv = Convert.ToInt32(row["LAST_INSERT_ID()"]);
                    }
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int Insert(SDK_VMS_SEND_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table} " +
                      $"(USER_ID, NOW_DATE, SEND_DATE, DEST_COUNT, DEST_INFO, CALLBACK, SUBJECT, MSG_SUBTYPE, RELISTEN_COUNT,  ATTACH_FILE, SEND_STATUS, RESERVED1) " +
                      $"VALUES('{vo.USER_ID}', '{vo.NOW_DATE}', '{vo.SEND_DATE}', '{vo.DEST_COUNT}', '{vo.DEST_INFO}', '{vo.CALLBACK}', '{vo.SUBJECT}', '{vo.MSG_SUBTYPE}', '{vo.RELISTEN_COUNT}', '{vo.ATTACH_FILE}', '{vo.SEND_STATUS}', '{vo.RESERVED1}') ";

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
