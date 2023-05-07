using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_EXTERN_VO
    {
        public string Key
        {
            get
            {
                return cols[0];
            }
        }

        public List<string> cols = new List<string>();

        public void Add(string value)
        {
            cols.Add(value);
        }

        public void Print()
        {
            string temp = string.Join(", ", cols);

            Console.WriteLine(temp);
        }
    }


}
