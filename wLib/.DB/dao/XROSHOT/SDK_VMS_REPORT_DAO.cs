using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class SDK_VMS_REPORT_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public SDK_VMS_REPORT_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "sdk_vms_report";
        }

        public IEnumerable<SDK_VMS_REPORT_VO> Select(string where = "1=1", string order = "MSG_ID", string limit = "1000")
        {
            List<SDK_VMS_REPORT_VO> list = new List<SDK_VMS_REPORT_VO>();

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
                        SDK_VMS_REPORT_VO vo = new SDK_VMS_REPORT_VO();

                        try
                        {
                            vo.MSG_ID = Convert.ToString(row["MSG_ID"]);
                        }
                        catch { }

                        try
                        {

                            vo.JOB_ID = Convert.ToString(row["JOB_ID"]);
                        }
                        catch { }

                        try
                        {
                            vo.USER_ID = Convert.ToString(row["USER_ID"]);
                        }
                        catch { }

                        try
                        {
                            vo.SEND_DATE = Convert.ToString(row["SEND_DATE"]);
                        }
                        catch { }

                        try
                        {
                            vo.DEST_COUNT = Convert.ToString(row["DEST_COUNT"]);
                        }
                        catch { }

                        try
                        {
                            vo.DEST_INFO = Convert.ToString(row["DEST_INFO"]);
                        }
                        catch { }

                        try
                        {
                            vo.CALLBACK = Convert.ToString(row["CALLBACK"]);
                        }
                        catch { }

                        try
                        {
                            vo.SUBJECT = Convert.ToString(row["SUBJECT"]);
                        }
                        catch { }

                        try
                        {
                            vo.MSG_SUBTYPE = Convert.ToString(row["MSG_SUBTYPE"]);
                        }
                        catch { }

                        try
                        {
                            vo.RELISTEN_COUNT = Convert.ToString(row["RELISTEN_COUNT"]);
                        }
                        catch { }

                        try
                        {
                            vo.ATTACH_FILE = Convert.ToString(row["ATTACH_FILE"]);
                        }
                        catch { }

                        try
                        {
                            vo.RESERVED1 = Convert.ToString(row["RESERVED1"]);
                        }
                        catch { }

                        try
                        {
                            vo.RESERVED2 = Convert.ToString(row["RESERVED2"]);
                        }
                        catch { }

                        try
                        {
                            vo.RESERVED3 = Convert.ToString(row["RESERVED3"]);
                        }
                        catch { }

                        try
                        {
                            vo.RESERVED4 = Convert.ToString(row["RESERVED4"]);
                        }
                        catch { }

                        try
                        {
                            vo.RESERVED5 = Convert.ToString(row["RESERVED5"]);
                        }
                        catch { }

                        try
                        {
                            vo.SUCC_COUNT = Convert.ToString(row["SUCC_COUNT"]);
                        }
                        catch { }

                        try
                        {
                            vo.FAIL_COUNT = Convert.ToString(row["FAIL_COUNT"]);
                        }
                        catch { }

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
    }
}
