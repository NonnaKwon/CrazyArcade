using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class ClientSession : PacketSession
    {
        public int SessionId { get; set; }
        public GameRoom Room { get; set; }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}/{SessionId}");
            Program.Room.Push(() => Program.Room.Enter(this));
        }
        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            Console.WriteLine($"OnRecvPacket : {SessionId}");
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            SessionManager.Instance.Remove(this);
            if(Room != null)
            {
                GameRoom room = Room;
                room.Push(() => room.Leave(this));
                Room = null; // 이따 jobQueue에 있는 아이들 Room을 찾지 못해서 크래쉬가 남.
            }
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }


        public override void OnSend(int numOfBytes)
        {
            //Console.WriteLine($"Transferred byted: {numOfBytes}");
        }
    }
}
