using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class SDK_VMS_REPORT_DETAIL_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public SDK_VMS_REPORT_DETAIL_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "sdk_vms_report_detail";
        }

        public IEnumerable<SDK_VMS_REPORT_DETAIL_VO> Select(string where = "1=1", string order = "MSG_ID", string limit = "1000")
        {
            List<SDK_VMS_REPORT_DETAIL_VO> list = new List<SDK_VMS_REPORT_DETAIL_VO>();

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
                        SDK_VMS_REPORT_DETAIL_VO vo = new SDK_VMS_REPORT_DETAIL_VO();

                        try
                        {
                            vo.MSG_ID = Convert.ToString(row["MSG_ID"]);
                        }
                        catch { }

                        try
                        {
                            vo.SUBJOB_ID = Convert.ToString(row["SUBJOB_ID"]);
                        }
                        catch { }

                        try
                        {
                            vo.USER_ID = Convert.ToString(row["USER_ID"]);
                        }
                        catch{ }

                        try
                        {
                            vo.SEND_DATE = Convert.ToString(row["SEND_DATE"]);
                        }
                        catch { }

                        try
                        {
                            vo.DEST_NAME = Convert.ToString(row["DEST_NAME"]);
                        }
                        catch { }

                        try
                        {
                            vo.PHONE_NUMBER = Convert.ToString(row["PHONE_NUMBER"]);
                        }
                        catch { }

                        try
                        {
                            vo.REPLIED_FILE = Convert.ToString(row["REPLIED_FILE"]);
                        }
                        catch { }

                        list.Add(vo);
                    }
                }
            }
            catch
            {

            }

            return list;
        }
    }
}
