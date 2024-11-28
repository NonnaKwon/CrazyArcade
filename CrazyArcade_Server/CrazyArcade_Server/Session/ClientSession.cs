using CrazyArcade_Server.Game;
using ServerCore;
using System.Net;

namespace GameServer
{
    public class ClientSession : PacketSession
    {
        public int SessionId { get; set; }
        public bool InRoom { get { return _currentRoom != null; } }
        public Player Player { get { return _player; } }
        public GameRoom Room { set { _currentRoom = value; } }

        private GameRoom _currentRoom;
        private Player _player;

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}/{SessionId}");
            Program.Lobby.Push(() => Program.Lobby.Enter(this));

            // TODO : 닉네임을 플레이어 정보와 연결해야함.
            _player = new Player(SessionId);

            S_Connect connect = new S_Connect();
            connect.id = SessionId;
            connect.nickname = _player.Nickname;
            Send(connect.Write());
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
