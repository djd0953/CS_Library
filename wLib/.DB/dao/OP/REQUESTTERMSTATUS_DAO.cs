using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class REQUESTTERMSTATUS_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public REQUESTTERMSTATUS_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "requesttermstatus";
        }

        public IEnumerable<REQUESTTERMSTATUS_VO> Select(string where = "1=1", string order = "CREATEDTM DESC", string limit = "1000")
        {
            List<REQUESTTERMSTATUS_VO> list = new List<REQUESTTERMSTATUS_VO>();

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
                        REQUESTTERMSTATUS_VO vo = new REQUESTTERMSTATUS_VO();
                        {
                            try
                            {
                                vo.termid = Convert.ToString(row["TERMID"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.ischeck = Convert.ToString(row["ISCHECK"]);
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

        // TODO RetDate
        public int Insert(REQUESTTERMSTATUS_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(TERMID, ISCHECK) " +
                      $"VALUES ({vo.termid}, 0) ";

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
