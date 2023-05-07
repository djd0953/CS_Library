using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHBSEND_DAO : DAO_T
    {
        public JHBSEND_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "jhbsend";
        }

        public IEnumerable<JHBSEND_VO> Select(string where = "1=1", string order = "JHSCode", string limit = "1000")
        {
            List<JHBSEND_VO> list = new List<JHBSEND_VO>();

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
                        try
                        {
                            JHBSEND_VO vo = new JHBSEND_VO
                            {
                                JHSCode = Convert.ToString(row["JHSCode"]), // AUTO
                                SendCode = Convert.ToString(row["SendCode"]),
                                JHACode = Convert.ToString(row["JHACode"]),
                                JHDeCode = Convert.ToString(row["JHDeCode"]),
                                RCMD = Convert.ToString(row["RCMD"]),
                                Parm1 = Convert.ToString(row["Parm1"]),
                                Parm2 = Convert.ToString(row["Parm2"]),
                                Parm3 = Convert.ToString(row["Parm3"]),
                                Parm4 = Convert.ToString(row["Parm4"]),
                                JHDate = Convert.ToString(row["JHDate"]),
                                JHSend = Convert.ToString(row["JHSend"]),
                                JHResponse = Convert.ToString(row["JHResponse"]),
                                JHReturn1 = Convert.ToString(row["JHReturn1"]),
                                JHReturn2 = Convert.ToString(row["JHReturn2"]),
                                JHReturn3 = Convert.ToString(row["JHReturn3"]),
                                JHReturn4 = Convert.ToString(row["JHReturn4"])
                            };

                            list.Add(vo);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        public int Insert(JHBSEND_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(SendCode, JHACode, JHDeCode, RCMD, Parm1, Parm2, Parm3, Parm4, JHDate) " +
                      $"VALUES('{vo.SendCode}', '{vo.JHACode}', '{vo.JHDeCode}', '{vo.RCMD}', '{vo.Parm1}', '{vo.Parm2}', '{vo.Parm3}', '{vo.Parm4}', '{vo.JHDate}') ";

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
