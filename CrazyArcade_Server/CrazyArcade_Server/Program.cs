
using CrazyArcade_Server.Game;
using Microsoft.Win32;
using ServerCore;
using System;
using System.Net;
using System.Text;

namespace GameServer
{
    class Program
    {
        static Listener _listener = new Listener();
        public static GameLobby Lobby = new GameLobby();

        static void FlushRoom()
        {
            Lobby.Push(() => Lobby.Flush());
            JobTimer.Instance.Push(FlushRoom, 250);
        }

        static void Main(string[] args)
        {
            string host = Dns.GetHostName(); //내 로컬 프로그램의 호스트 이름
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0]; //ip가 여러개일수도 있다.(구글같이 트래픽이 어마어마한 사이트) 그래서 배열로 뱉어줌.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 식당주소, 식당문번호(port) 라고 생각.

            _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            Console.WriteLine("Listening...");

            //FlushRoom();
            JobTimer.Instance.Push(FlushRoom);

            while (true)
            {
                JobTimer.Instance.Flush();
            }

        }
    }
}