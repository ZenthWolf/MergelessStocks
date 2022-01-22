using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace Zenth_MergelessStocks
{
    class MergelessStocks : Mod
    {
        public static MergelessStocks Instance;
        public bool test = true;

        public MergelessStocks(ModContentPack content) : base(content)
        {
            if(MergelessStocks.Instance == null)
            {
                MergelessStocks.Instance = this;
            }
        }
    }
}
