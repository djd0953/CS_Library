using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class ORDERRESULTVIEW_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public ORDERRESULTVIEW_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
        }

        public IEnumerable<ORDERRESULTVIEW_VO> Select(string where = "1=1", string order = "ORDERNUM DESC", string limit = "1000")
        {
            string sql;
            List<ORDERRESULTVIEW_VO> list = new List<ORDERRESULTVIEW_VO>();

            try
            {
                sql = $"SELECT b.*, a.REQUESTORDERID " +
                      $"FROM orderlistview AS a LEFT JOIN orderresultview AS b ON a.ORDERNUM = b.ORDERNUM " +
                      $"WHERE {where} ORDER BY b.{order} LIMIT {limit}";

                DataTable dt = base.Select(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ORDERRESULTVIEW_VO vo = new ORDERRESULTVIEW_VO();
                        {
                            try
                            {
                                vo.ordernum = Convert.ToString(row["ORDERNUM"]);
                            }
                            catch { }

                            try
                            {
                                vo.termid = Convert.ToString(row["TERMID"]);
                            }
                            catch { }

                            try
                            {
                                vo.respoonse = Convert.ToString(row["RESPONSE"]);
                            }
                            catch { }

                            try
                            {
                                vo.result = Convert.ToString(row["RESULT"]);
                            }
                            catch { }

                            try
                            {
                                vo.requestorderId = Convert.ToString(row["REQUESTORDERID"]);
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
