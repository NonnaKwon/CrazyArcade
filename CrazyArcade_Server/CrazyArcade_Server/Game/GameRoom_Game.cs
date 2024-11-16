using ServerCore;

namespace CrazyArcade_Server.Game
{
    partial class GameRoom : IJobQueue
    {
        public int Id { get { return _id; } }
        public string RoomName { get { return _roomName; } }
        public int Map { get { return _map; } }
        public int PlayerCount { get { return _playerCount; } }
        public bool IsStart { get { return _isStart; } }

        private int _id;
        private string _roomName;
        private int _map;
        private int _playerCount;
        private bool _isStart;
    }
}
