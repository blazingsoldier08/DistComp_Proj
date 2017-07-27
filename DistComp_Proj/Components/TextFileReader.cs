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
        /// <summary>
        /// Everything is static so you don't have to instantiate the object
        /// 
        /// Assign the processID to TextFileReader.processID
        /// Next call the function TextFileReader.FindIPFromFile()
        /// 
        /// Allows you to call the string attributes: 
        /// TextFileReader.processID
        /// TextFileReader.localIP
        /// TextFileReader.localPortNum
        /// </summary>

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
        

        private static Dictionary<int, ProcessInfo> _dict = null;
        public static Dictionary<int, ProcessInfo> dict
        {
            get { return _dict; }
            set { _dict = value; }
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
                dict = new Dictionary<int, ProcessInfo>();
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
                        dict.Add(int.Parse(processID), new ProcessInfo() { portNumber = localPortNum, ipAddress = localIP });
                    }
                    else
                    {
                        int processIDEndIndex;
                        int endOfIPIndex;
                        string procID;
                        string portNum;
                        string ipAddr;
                        processIDEndIndex = currentLine.IndexOf("\t");
                        procID = currentLine.Substring(0, processIDEndIndex);
                        // +1 accounts for the first tab character i.e. find the tab then start at the next character.
                        endOfIPIndex = currentLine.Substring((processIDEndIndex + 1)).IndexOf("\t");
                        ipAddr = currentLine.Substring(processIDEndIndex + 1, endOfIPIndex);
                        // +2 accounts for the tab characters
                        portNum = currentLine.Substring(processIDEndIndex + endOfIPIndex + 2);
                        dict.Add(int.Parse(procID), new ProcessInfo() { portNumber = portNum, ipAddress = ipAddr });
                    }
                }
                
            }
        }

        #endregion
    }
}
