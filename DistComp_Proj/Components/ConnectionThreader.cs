using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace DistComp_Proj.Components
{
    public class ConnectionThreader
    {
        /* 
         1. Create a server thread and listen
             */

        IPAddress _localIPAddress;
        string _localIPAddressString;
        int _localPortNum;
        int _localProcessNum;
        Dictionary<int, ProcessInfo> _dict;
        TcpListener srvr;
        List<TcpClient> clients;

        public ConnectionThreader(int localProcessNum, Dictionary<int, ProcessInfo> dict)
        {
            _localProcessNum = localProcessNum;
            _localPortNum = int.Parse(dict[_localProcessNum].portNumber);
            _dict = dict;
            _localIPAddressString = _dict[_localProcessNum].ipAddress;
            _localIPAddress = IPAddress.Parse(_localIPAddressString);
            srvr = new TcpListener(_localIPAddress, _localPortNum);
            clients = new List<TcpClient>();
            ConnectToRemainingProcesses();
        }
        public void ConnectProcesses()
        {
            int dictionaryLength = _dict.Count;
            bool Active = true;
            srvr.Start();


            Console.WriteLine("TCP: " + srvr.Server.Handle);
            Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
            var clock = new System.Timers.Timer(10000);
            clock.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, ElapsedEventArgs e){ Active = false; });
            clock.Enabled = true;
            while (Active)
            {
                if(srvr.Pending())
                {
                    TcpClient client = srvr.AcceptTcpClient();
                    clients.Add(client);
                    Thread thr = new Thread(TCPClientThread);
                    thr.Start(client);
                }
            }
            Random seed = new Random();

            while (true)
            {
                double d = seed.Next(0, 100);
                if (d >= 0.5)
                {
                    foreach (var c in clients)
                    {
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(_localProcessNum + ", Hello");
                        c.GetStream().Write(data, 0, data.Length);
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void ConnectToRemainingProcesses()
        {
            for(int i = (_localProcessNum + 1); i < 11 ; i++ )
            {
                if(_dict.ContainsKey(i))
                {
                    try
                    {
                        TcpClient t = new TcpClient(_dict[i].ipAddress, int.Parse(_dict[i].portNumber));
                        clients.Add(t);
                        Thread thr = new Thread(TCPClientThread);
                        thr.Start(t);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
        }
        private void TCPClientThread(object t)
        {
            var tcp = (TcpClient)t;
            NetworkStream strm = tcp.GetStream();
            Byte[] data = new Byte[20];
            int size;
            string message;
            Console.WriteLine("TCP: " + tcp.Client.Handle);
            Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
            while (tcp.Connected)
            {
                size = strm.Read(data, 0, data.Length);
                message = System.Text.Encoding.ASCII.GetString(data, 0, size);
                Console.WriteLine(message);
            }
            
        }


    }
}
