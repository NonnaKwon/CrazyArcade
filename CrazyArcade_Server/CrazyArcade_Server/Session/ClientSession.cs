using CrazyArcade_Server.Game;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class ClientSession : PacketSession
    {
        public int SessionId { get; set; }
        private GameRoom _curRoom;

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}/{SessionId}");
            Program.Lobby.Push(() => Program.Lobby.Enter(this));
        }
        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            Console.WriteLine($"OnRecvPacket : {SessionId}");
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            SessionManager.Instance.Remove(this);
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }


        public override void OnSend(int numOfBytes)
        {
            //Console.WriteLine($"Transferred byted: {numOfBytes}");
        }
    }
}
