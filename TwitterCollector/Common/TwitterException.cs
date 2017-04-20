using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            db.WriteExceptionToDB(Path.GetFileName(filePath), GetExceptionFullName(e), GetExceptionLineNumber(e), message);
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

        /// <summary>
        /// Get the line number where the exception was occurred
        /// </summary>
        /// <param name="e">The exception object was thrown</param>
        /// <returns>Line number</returns>
        public static int GetExceptionLineNumber(Exception e)
        {
            // Get stack trace for the exception with source file information
            var st = new StackTrace(e, true);
            // Get the top stack frame
            var frame = st.GetFrame(st.FrameCount - 1);
            // Get the line number from the stack frame
            return frame.GetFileLineNumber();
        }

        /// <summary>
        /// Get the function full name where the exception was occurred
        /// </summary>
        /// <param name="e">The exception object was thrown</param>
        /// <returns>Exception full path</returns>
        public static string GetExceptionFullName(Exception e)
        {
            // Get stack trace for the exception with source file information
            StackTrace st = new StackTrace(e, true);
            // Get the top stack frame
            var frame = st.GetFrame(st.FrameCount - 1);
            var Class = frame.GetMethod().ReflectedType;
            var Namespace = Class.Namespace;         //Added finding the namespace
            return Namespace + "." + Class.Name + "." + frame.GetMethod().Name;
        }
    }
}
