using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHAREA_DAO : DAO_T
    {

        public JHAREA_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "jharea";
        }

        public List<JHAREA_VO> GetAreaList()
        {
            return Select("UPPER(JHComModel) IN('GATE_LAN') " +
                          "AND UPPER(JHOnOff) IN('ON')"
                          ).ToList();
        }

        public IEnumerable<JHAREA_VO> Select(string where = "1=1", string order = "JHAreaCode", string limit = "1000")
        {
            List<JHAREA_VO> list = new List<JHAREA_VO>();

            // 표준
            // 01: 강우량계
            // 02: 수위계
            // 03: 변위계
            // 04: 토양함수비계
            // 05: 거리측정기
            // 06: 적설계
            // 07: 지하수위계
            // 08: 경사계
            // 09: 간극수압계
            // 10: 진동계
            // 11: 지중경사계
            // 12: 하중계
            // 13: 구조물경사계
            // 14: GNSS(GPS)
            // 15: 지표변위계
            // 16: 조위계

            // 비표준
            // 17: 방송
            // 18: 전광판
            // 19: CCTV
            // 20: 차단기
            // 21: 침수

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
                        JHAREA_VO vo = new JHAREA_VO();
                        try
                        {

                            vo.JHAreaCode = Convert.ToString(row["JHAreaCode"]);
                        }
                        catch { }

                        try
                        {
                            //vo.JHName = Convert.ToString(row["JHName"]);
                            vo.JHName = Encoding.Default.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(Convert.ToString(row["JHName"])));
                        }
                        catch { }

                        try
                        {
                            vo.JHComSys = Convert.ToString(row["JHComSys"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHComModel = Convert.ToString(row["JHComModel"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHIP = Convert.ToString(row["JHIP"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHPort = Convert.ToString(row["JHPort"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHPhone = Convert.ToString(row["JHPhone"]);
                        }
                        catch { }

                        try
                        {
                            if (row["JHLastDate"] is DateTime)
                            {
                                vo.JHLastDate = Convert.ToDateTime(row["JHLastDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else vo.JHLastDate = Convert.ToString(row["JHLastDate"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHLastStatus = Convert.ToString(row["JHLastStatus"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHSelect = Convert.ToString(row["JHSelect"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHOnOff = Convert.ToString(row["JHOnOff"]);
                        }
                        catch { }

                        try
                        {
                            vo.JHRainBit = Convert.ToString(row["JHRainBit"]);
                        }
                        catch { }

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
