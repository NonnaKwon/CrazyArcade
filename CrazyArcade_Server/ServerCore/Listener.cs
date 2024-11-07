using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    public class Listener
    {
        Socket _listenSocket;
        Func<Session> _sessionFactory;

        public void Init(IPEndPoint endPoint, Func<Session> sessionFactory,int register = 10, int backlog = 100)
        { 
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _sessionFactory = sessionFactory;
             
            //문지기 교육
            _listenSocket.Bind(endPoint);

            //영업 시작
            //backing : 최대 대기수
            _listenSocket.Listen(backlog); //이 숫자를 초과하게 되면 fail이 뜨게 됨

            //문지기 여러명 등록
            for(int i=0;i< register;i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted); //콜백 등록
                RegisterAccept(args);
            }
        }

        void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

            bool pending =_listenSocket.AcceptAsync(args);
            if (pending == false) //false일때 바로 클라이언트가 접속해서 바로 받아진것이라는 뜻.(낚시대를 던지자마자 바로 물고기 잡힌거)
                OnAcceptCompleted(null, args);
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if(args.SocketError == SocketError.Success) //에러가 없이 잘 처리가 되었다.
            {
                Session session = _sessionFactory.Invoke();
                session.Start(args.AcceptSocket);
                session.OnConnected(args.AcceptSocket.RemoteEndPoint);
            }
            else
                Console.WriteLine(args.SocketError.ToString());

            RegisterAccept(args);
        }

    }
}
