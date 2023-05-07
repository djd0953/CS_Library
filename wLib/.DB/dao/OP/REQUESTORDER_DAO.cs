using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class REQUESTORDER_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public REQUESTORDER_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "requestorder";
        }

        public IEnumerable<REQUESTORDER_VO> Select(string where = "1=1", string order = "SendCode", string limit = "1000")
        {
            List<REQUESTORDER_VO> list = new List<REQUESTORDER_VO>();

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
                        REQUESTORDER_VO vo = new REQUESTORDER_VO();
                        {
                            try
                            {
                                vo.requestorderId = Convert.ToString(row["REQUESTORDERID"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.mode = Convert.ToString(row["MODE"]);
                            }
                            catch { }

                            try
                            {
                                vo.mediatype = Convert.ToString(row["MEDIATYPE"]);
                            }
                            catch { }

                            try
                            {
                                vo.disastermsg = Convert.ToString(row["DISASTERMSG"]);
                            }
                            catch { }

                            try
                            {
                                vo.listtermid = Convert.ToString(row["LISTTERMID"]);
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
        public int Insert(REQUESTORDER_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(MODE, MEDIATYPE, DISASTERMSG, LISTTERMID)" +
                      $"VALUES('{vo.mode}', '{vo.mediatype}', '{vo.disastermsg}', '{vo.listtermid}') ";

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
