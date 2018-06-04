using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaleEditor.ConfigStore
{
    static class Globals
    {
        private static Double viewSize = 1;
        public static Double ViewSize
        {
            get { return viewSize;  }
            set { viewSize = value; }
        }
    }
}
