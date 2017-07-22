using DistComp_Proj.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistComp_Proj
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TestFileReading



            #endregion

            #region TestPathing

            Console.WriteLine(Directory.GetCurrentDirectory());
            Logger.processID = args[0];
            Console.WriteLine(Logger.processID);
            Console.WriteLine(Logger.path);
            Logger.LogMessage("1, 2, 3", "test message");
            Console.ReadLine();
            
            #endregion
        }
    }
}
