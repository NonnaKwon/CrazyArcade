using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
    //ID는 서버에서 관리함. 서버의 세션매니저와 역할이 다르다.
    class SessionManager
    {
        static SessionManager _session = new SessionManager();
        public static SessionManager Instance { get { return _session; } }

        List<ServerSession> _sessions = new List<ServerSession>();
        object _lock = new object();
        Random _rand = new Random();

        public void SendForEach()
        {
            lock(_lock)
            {
                foreach(ServerSession session in _sessions)
                {
                    C_Move movePacket = new C_Move();
                    movePacket.posX = _rand.Next(-50, 50);
                    movePacket.posY = 0;
                    movePacket.posZ = _rand.Next(-50, 50);

                    session.Send(movePacket.Write());
                }
            }
        }

        public ServerSession Generate()
        {
            lock(_lock)
            {
                ServerSession session = new ServerSession();
                _sessions.Add(session);
                return session;
            }
        }

        public void Disconnect(ServerSession session)
        {
            _sessions.Remove(session);
        }
    }
}
