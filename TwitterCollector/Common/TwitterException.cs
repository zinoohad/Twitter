using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Common
{
    public class TwitterException : Exception
    {
        public TwitterException(string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            Console.WriteLine(DateTime.Now + " : " + message);
        }
        public TwitterException(Exception e, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            Console.WriteLine(DateTime.Now + " : " + e.Message);
        }
    }
}
