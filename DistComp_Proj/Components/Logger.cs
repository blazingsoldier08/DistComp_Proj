using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistComp_Proj.Components
{
    public class Logger
    {
        #region SetAttributes

        private static string _processID = null;
        public static string processID
        {
            get { return _processID; }
            set { _processID = value; }
        }
        public static string path
        {
            get { return Directory.GetCurrentDirectory() + "\\" + processID + "_msg.txt"; }
        }
        private static StreamWriter sw = null;
        public static StreamWriter StreamWriter
        {
            get { return sw; }
            set { sw = value; }
        }

        #endregion

        #region

        public static void LogMessage(string bssClock, string message)
        {
            using (StreamWriter = File.AppendText(path))
            {
                StreamWriter.WriteLine(DateTime.Now + "\n \t Clock Value: " + bssClock + " \n \t Message: " + message);
            }
        }

        #endregion
    }
}
