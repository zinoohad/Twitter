using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Common
{
    public class TwitterException : Exception
    {

        private DBHandler db = Global.DB;

        public TwitterException(string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            if(message.Length > 500)           
                message = "..." + message.Substring(message.Length - 495);           
            db.WriteExceptionToDB(Path.GetFileName(filePath), caller, lineNumber, message);
        }

        public TwitterException(Exception e, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            string message = GetInnerExceptions(e);
            if (message.Length > 500)
                message = "..." + message.Substring(message.Length - 495);
            db.WriteExceptionToDB(Path.GetFileName(filePath), caller, lineNumber, message);
        }

        private string GetInnerExceptions(Exception e)
        {
            if (e.InnerException == null) 
                return e.Message;

            StringBuilder s = new StringBuilder();
            while (e != null)
            {
                s.AppendLine(e.Message + System.Environment.NewLine);
                e = e.InnerException;
            }
            return s.ToString();
        }
    }
}
