using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHEQUIPSTATUS_DAO : DAO_T
    {
        public JHEQUIPSTATUS_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "jhequipstatus";
        }

        public IEnumerable<JHEQUIPSTATUS_VO> Select(string where = "1=1", string order = "JHSCode", string limit = "1000")
        {
            List<JHEQUIPSTATUS_VO> list = new List<JHEQUIPSTATUS_VO>();

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
                            JHEQUIPSTATUS_VO vo = new JHEQUIPSTATUS_VO
                            {
                                JHSCode = Convert.ToString(row["JHSCode"]), // AUTO
                                JHACode = Convert.ToString(row["JHACode"]),
                                JHDeCode = Convert.ToString(row["JHDeCode"]),
                                Volume = Convert.ToString(row["Volume"]),
                                Output = Convert.ToString(row["Output"]),
                                Relay = Convert.ToString(row["Relay"]),
                                Bell = Convert.ToString(row["Bell"]),
                                PlayTime = Convert.ToString(row["PlayTime"]),
                                JHDate = Convert.ToString(row["JHDate"]),
                                SendOK = Convert.ToString(row["SendOK"])
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

        public int Insert(JHEQUIPSTATUS_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}(JHACode, JHDeCode, Volume, Output, Relay, Bell, PlayTime, JHDate, SendOK) " +
                      $"VALUES('{vo.JHACode}', '{vo.JHDeCode}', '{vo.Volume}', '{vo.Output}', '{vo.Relay}', '{vo.Bell}', '{vo.PlayTime}', '{vo.JHDate}', '{vo.SendOK}') ";

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
