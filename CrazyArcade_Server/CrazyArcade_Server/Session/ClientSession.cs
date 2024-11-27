using CrazyArcade_Server.Game;
using ServerCore;
using System.Net;

namespace GameServer
{
    public partial class ClientSession : PacketSession
    {
        public int SessionId { get; set; }
        private GameRoom _cureentRoom;

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}/{SessionId}");
            Program.Lobby.Push(() => Program.Lobby.Enter(this));
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            Console.WriteLine($"OnRecvPacket : {SessionId} / {(PacketID)BitConverter.ToUInt16(buffer.Array,buffer.Offset+2)}");
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
