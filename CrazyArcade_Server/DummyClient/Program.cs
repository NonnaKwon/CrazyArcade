using ServerCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName(); //내 로컬 프로그램의 호스트 이름
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0]; //ip가 여러개일수도 있다.(구글같이 트래픽이 어마어마한 사이트) 그래서 배열로 뱉어줌.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 식당주소, 식당문번호(port) 라고 생각.

            Connector connetor = new Connector();
            connetor.Connect(endPoint, 
                () => { return SessionManager.Instance.Generate(); },
                10); //10개 접속하고싶다. 

            while(true)
            {
                try
                {
                    SessionManager.Instance.SendForEach();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Thread.Sleep(250);
            }
            
        }
    }
}