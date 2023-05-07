using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHEQUIP_DAO : DAO_T
    {
        public JHEQUIP_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "jhequip";
        }

        public IEnumerable<JHEQUIP_VO> Select(string where = "1=1", string order = "JHECode", string limit = "1000")
        {
            List<JHEQUIP_VO> list = new List<JHEQUIP_VO>();

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
                            JHEQUIP_VO vo = new JHEQUIP_VO
                            {
                                JHECode = Convert.ToString(row["JHECode"]), // AUTO
                                JHACode = Convert.ToString(row["JHACode"]),
                                JHDeCode = Convert.ToString(row["JHDeCode"]),
                                JHName = Convert.ToString(row["JHName"]),
                                JHPhone = Convert.ToString(row["JHPhone"])
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

        public int Insert(JHEQUIP_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(JHACode, JHDeCode, JHName, JHPhone) " +
                      $"VALUES('{vo.JHACode}', '{vo.JHDeCode}', '{vo.JHName}', '{vo.JHPhone}') ";

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
