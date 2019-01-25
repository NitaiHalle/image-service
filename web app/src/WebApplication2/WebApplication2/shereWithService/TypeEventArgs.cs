using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shered
{
    public class TypeEventArgs:EventArgs
    {
        public int TypeArgs { set; get; }
        public string[] Args { set; get; }
        public TypeEventArgs(int type, string[] args)
        {
            TypeArgs = type;
            Args = args;
        }
    }
}
