using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public interface AREA_LIST_VO
    {
        string Code { get; set; }
        string Name { get; set; }
        string Ip { get; set; }
        string Port { get; set; }
        string Status { get; set; }
        string Data { get; set; }
    }
}
