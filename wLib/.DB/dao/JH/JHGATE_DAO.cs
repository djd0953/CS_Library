using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHGATE_DAO : DAO_T
    {
        public JHGATE_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "jhgate";
        }

        public IEnumerable<JHGATE_VO> Select(string where = "1=1", string order = "JHAreaCode", string limit = "1000")
        {
            List<JHGATE_VO> list = new List<JHGATE_VO>();

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
                        JHGATE_VO vo = new JHGATE_VO();
                        {
                            try
                            {
                                vo.JHGSCode = Convert.ToString(row["JHGSCode"]); // AUTO_PK
                            }
                            catch { }

                            try
                            {
                                vo.JHAreaCode = Convert.ToString(row["JHAreaCode"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHGate = Convert.ToString(row["JHGate"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHLight = Convert.ToString(row["JHLight"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHSound = Convert.ToString(row["JHSound"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHStatus = Convert.ToString(row["JHStatus"]);
                            }
                            catch { }

                            try
                            {
                                if (row["JHDate"] is DateTime)
                                {
                                    vo.JHDate = Convert.ToDateTime(row["JHDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    vo.JHDate = Convert.ToString(row["JHDate"]);
                                }
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
