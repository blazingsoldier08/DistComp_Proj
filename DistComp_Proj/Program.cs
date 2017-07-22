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
            InitializeComponents(args);
            
            //TestFileReading();

            //TestPathing();

        }

        public static void InitializeComponents(string[] args)
        {
            TextFileReader.processID = args[0];
            Logger.processID = args[0];
        }

        public static void TestPathing()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(Logger.processID);
            Console.WriteLine(Logger.path);
            Logger.LogMessage("1, 2, 3", "test message");
            Console.ReadLine();
        }
        public static void TestFileReading()
        {
            TextFileReader.FindIPFromFile();
            Console.WriteLine(TextFileReader.processID);
            Console.WriteLine(TextFileReader.localIP);
            Console.WriteLine(TextFileReader.localPortNum);
            Console.ReadLine();
        }
    }
}
