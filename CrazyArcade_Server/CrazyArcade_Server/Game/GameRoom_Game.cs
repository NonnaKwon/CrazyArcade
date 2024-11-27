using ServerCore;

namespace CrazyArcade_Server.Game
{
    public partial class GameRoom : IJobQueue
    {
        public int Id { get { return _id; } }
        public string RoomName { get { return _roomName; } set { _roomName = value; } }
        public int Map { get { return _map; } set { _map = value; } }
        public int PlayerCount { get { return _sessions.Count; } }
        public int MaxPlayer { get { return _maxPlayer; } set { _maxPlayer = value; } }
        public bool IsStart { get { return _isStart; } set { _isStart = value; } }


        private int _id;
        private string _roomName;
        private int _map;
        private int _maxPlayer;
        private bool _isStart;

        private static int _roomId = 0;
        public GameRoom()
        {
            _id = ++_roomId;
            _map = (int)Define.Map.Block;
            _isStart = false;
            _roomName = "";
        }
    }
}
