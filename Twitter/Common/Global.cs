using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Common
{
    static class Global
    {
        public static bool HasHebrewCharacters(string str)
        {
            for (int i = 0; i < str.Length; i++)
                if ('א' <= str[i] && str[i] <= 'ת') return true;         
            return false;
        }
        public static bool IsNullOrEmpty(params string[] values)
        {
            foreach (string s in values)
                if (string.IsNullOrEmpty(s.Trim()))
                    return true;
            return false;
        }
    }
}
