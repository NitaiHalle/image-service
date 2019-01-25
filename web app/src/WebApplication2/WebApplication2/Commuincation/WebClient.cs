using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shered;
namespace WebApplication2.Commuincation
{
    public class WebClient : IWebClient
    {
        private int port = 45000;
        private Mutex mutex = new Mutex();
        private object l = new object();
        private TcpClient tcpClient;
        private NetworkStream stream;
        private BinaryWriter writer;
        private BinaryReader reader;


        private static WebClient singelton;
        private WebClient()
        {
            start();
            getData();
        }
        public static WebClient singeltonClient()
        {
            if (singelton == null)
            {
                singelton = new WebClient();
            }
            return singelton;
        }
        public event EventHandler<TypeEventArgs> OnDataRecived;

        public void start()
        {
            if (tcpClient == null)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                tcpClient = new TcpClient();
                try
                {
                    tcpClient.Connect(endPoint);
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }
        public void reconnect()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(endPoint);
            }
            catch (Exception e)
            {
                return;
            }
        }
        public void stop()
        {
            tcpClient.Close();
        }
        private void getData()
        {
            string args;
            new Task(() =>
            {
                if (tcpClient.Connected)
                {
                    stream = tcpClient.GetStream();
                    reader = new BinaryReader(stream);
                }
                while (tcpClient.Connected)
                {
                    try
                    {
                        args = reader.ReadString();
                        Debug.Write(args);

                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                    TypeEventArgs e = JsonConvert.DeserializeObject<TypeEventArgs>(args);
                    OnDataRecived?.Invoke(this, e);

                }

            }).Start();
        }

       


        public void send(object sender, TypeEventArgs e)
        {
            string args = JsonConvert.SerializeObject(e);
            new Task(() =>
            {
                if (tcpClient.Connected)
                {
                    stream = tcpClient.GetStream();
                    writer = new BinaryWriter(stream);
                    try
                    {
                        mutex.WaitOne();

                        writer.Write(args);
                        mutex.ReleaseMutex();
                    }
                    catch (Exception exc)
                    {
                        mutex.ReleaseMutex();
                        return;
                    }
                }
            }).Start();
        }
        public bool connected()
        {
            return tcpClient.Connected;
        }

    }
}




