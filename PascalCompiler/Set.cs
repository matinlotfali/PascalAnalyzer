using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalCompiler
{
    class Set
    {
        List<Tokens> list;
        public Set()
        {
            list = new List<Tokens>();
        }

        public static Set operator +(Set set, Tokens t)
        {
            Set a = new Set();
            a.list = set.list.ToList();
            a.Add(t);
            return a;
        }

        public static Set Null()
        {
            Set a = new Set();
            a.Add(Tokens.s_eof);
            return a;
        }

        public void Add(Tokens t)
        {
            if (!IsExist(t))
                list.Add(t);
        }

        public bool IsExist(Tokens t)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == t)
                    return true;
            return false;
        }
    }
}
