using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHGATESTATUS_DAO : DAO_T
    {
        public JHGATESTATUS_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "jhgatestatus";
        }

        public IEnumerable<JHGATESTATUS_VO> Select(string where = "1=1", string order = "JHAreaCode", string limit = "1000")
        {
            List<JHGATESTATUS_VO> list = new List<JHGATESTATUS_VO>();

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
                        JHGATESTATUS_VO vo = new JHGATESTATUS_VO();
                        {
                            try
                            {
                                vo.JHAreaCode = Convert.ToString(row["JHAreaCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                if (row["JHDate"] is DateTime)
                                {
                                    vo.JHDate = Convert.ToDateTime(row["JHDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                    vo.JHDate = Convert.ToString(row["JHDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHAuto = Convert.ToString(row["JHAuto"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHGate = Convert.ToString(row["JHGate"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHLightUse = Convert.ToString(row["JHLightUse"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHLight = Convert.ToString(row["JHLight"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHSoundUse = Convert.ToString(row["JHSoundUse"]);
                            }
                            catch { }

                            try
                            {
                                vo.JHSound = Convert.ToString(row["JHSound"]);
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
