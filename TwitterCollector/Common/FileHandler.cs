using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Common
{
    public static class FileHandler
    {
        public static List<string> ReadFileLines(string filePath, Func<string,string> func = null)
        {
            List<string> file = new List<string>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string input, funcResult;
                    while ((input = sr.ReadLine()) != null)
                    {
                        if (func != null)
                        {
                            if ((funcResult = func(input)) != null)
                                file.Add(funcResult);
                        }
                        else file.Add(input);
                    }
                    return file;
                }
            }
            else throw new Exception("File path not valid.");
        }
    }
}
