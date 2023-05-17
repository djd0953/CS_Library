using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace wLib.DB
{
    public class AREA_LIST_VO : IAREA_LIST_I, IComparable<IAREA_LIST_I>
    {
        public virtual string CD_DIST_OBSV { get; set; }
        public virtual string SUB_OBSV { get; set; }

        public virtual string NM_DIST_OBSV { get; set; } = "";
        public virtual string GB_OBSV { get; set; }
        public virtual string MODEL { get; set; }

        public virtual string PHONE { get; set; }
        public virtual string IP { get; set; }
        public virtual string PORT { get; set; }

        public virtual string DATE  { get; set; }
        public virtual string STATUS { get; set; }
        public virtual string STATUS_KO
        {
            get
            {
                switch(STATUS.ToLower())
                {
                    case "start":
                        return "작업시작";
                    case "ing":
                        return "작업중";
                    case "end":
                        return "작업완료";

                    case "ok":
                        return "작업완료";
                    case "fail":
                        return "작업실패";
                    case "error":
                        return "작업오류";

                    default:
                        return "알수없음";
                }
            }
        }
        public virtual string USE_YN { get; set; }

        // DATA 영역
        public virtual string FACTOR { get; set; } // 곱하기 연산
        public virtual string OFFSET { get; set; } // 더하기 연산
        public virtual string DATA { get; set; } // 데이터

        public int CompareTo(IAREA_LIST_I other)
        {
            try
            {
                if (int.TryParse(this.CD_DIST_OBSV, out _))
                {
                    return (int.Parse(this.CD_DIST_OBSV) < int.Parse(other.CD_DIST_OBSV))? 1: -1;
                }

                return string.Compare(this.CD_DIST_OBSV, other.CD_DIST_OBSV);
            }
            catch
            {
                return string.Compare(this.CD_DIST_OBSV, other.CD_DIST_OBSV);
            }
        }
    }

    public interface IAREA_LIST_I
    {
        string CD_DIST_OBSV { get; set; } // PK
        string SUB_OBSV { get; set; } // PK

        string NM_DIST_OBSV { get; set; }
        string GB_OBSV { get; set; }
        string MODEL { get; set; }

        string PHONE { get; set; }
        string IP { get; set; }
        string PORT { get; set; }

        string DATE { get; set; }
        string STATUS { get; set; }
        string STATUS_KO { get; }
        
        string USE_YN { get; set; }
        

        // DATA 영역
        string FACTOR { get; set; } // 곱하기 연산
        string OFFSET { get; set; } // 더하기 연산
        string DATA { get; set; } // 데이터
    }
}
