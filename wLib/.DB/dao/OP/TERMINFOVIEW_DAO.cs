using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wLib;

namespace wLib.DB
{
    public class TERMINFOVIEW_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public TERMINFOVIEW_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "terminfoview";
        }

        public IEnumerable<TERMINFOVIEW_VO> Select(string where = "1=1", string order = "TERMID", string limit = "1000")
        {
            List<TERMINFOVIEW_VO> list = new List<TERMINFOVIEW_VO>();

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
                        TERMINFOVIEW_VO vo = new TERMINFOVIEW_VO();
                        {
                            try
                            {
                                vo.termid = Convert.ToString(row["TERMID"]);
                            }
                            catch { }

                            try
                            {
                                vo.regionname = Convert.ToString(row["REGIONNAME"]);
                            }
                            catch { }

                            try
                            {
                                vo.name = Convert.ToString(row["Name"]);
                            }
                            catch { }

                            try
                            {
                                vo.address = Convert.ToString(row["ADDRESS"]);
                            }
                            catch { }

                            try
                            {
                                vo.lat = Convert.ToString(row["LATITUDE"]);
                            }
                            catch { }

                            try
                            {
                                vo.lon = Convert.ToString(row["LONGITUDE"]);
                            }
                            catch { }

                            try
                            {
                                vo.status = Convert.ToString(row["STATUS"]);
                            }
                            catch { }

                            try
                            {
                                vo.lastcheckdtm = Convert.ToString(row["LASTCHECKDTM"]);
                            }
                            catch { }
                        }
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
