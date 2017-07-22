using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistComp_Proj.Components
{
    public class TextFileReader
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
            get { return Directory.GetCurrentDirectory() + "\\" + "setup.txt"; }
        }
        private static StreamReader sr = null;
        public static StreamReader StreamReader
        {
            get { return sr; }
            set { sr = value; }
        }

        #endregion

        #region StringTestAttributes

        private static string _currentLine = null;
        private static string currentLine
        {
            get { return _currentLine; }
            set { _currentLine = value; }
        }

        private static string _localIP = null;
        public static string localIP
        {
            get { return _localIP; }
            set { _localIP = value; }
        }

        private static string _localPortNum = null;
        public static string localPortNum
        {
            get { return _localPortNum; }
            set { _localPortNum = value; }
        }

        #endregion

        #region ReadTextFile

        public static void FindIPFromFile()
        {
            using (StreamReader = File.OpenText(path))
            {
                while(!StreamReader.EndOfStream)
                {
                    currentLine = StreamReader.ReadLine();
                    if (currentLine.Substring(0, processID.Length) == processID && currentLine[processID.Length]=='\t')
                    {
                        int processIDEndIndex;
                        int endOfIPIndex;
                        processIDEndIndex = currentLine.IndexOf("\t");
                        // +1 accounts for the first tab character i.e. find the tab then start at the next character.
                        endOfIPIndex = currentLine.Substring((processIDEndIndex + 1)).IndexOf("\t");
                        localIP = currentLine.Substring(processIDEndIndex + 1, endOfIPIndex);
                        // +2 accounts for the tab characters
                        localPortNum = currentLine.Substring(processIDEndIndex + endOfIPIndex + 2);

                    }
                }
                
            }
        }

        #endregion
    }
}
