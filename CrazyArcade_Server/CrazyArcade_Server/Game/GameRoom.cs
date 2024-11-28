using GameServer;
using ServerCore;

namespace CrazyArcade_Server.Game
{
    public partial class GameRoom : IJobQueue
    {
        private List<ClientSession> _sessions = new List<ClientSession>();
        private JobQueue _jobQueue = new JobQueue();
        private List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        public void Flush()
        {
            foreach (ClientSession s in _sessions)
                s.Send(_pendingList);

            //Console.WriteLine($"Flused {_pendingList.Count} Items");
            _pendingList.Clear();
        }

        public void Broadcast(ArraySegment<byte> segment)
        {
            _pendingList.Add(segment);
        }

        public void Enter(ClientSession session)
        {
            if (_sessions.Count > Define.MAX_PLAYER)
            {
                // 들어올 수 없다는 패킷을 날린다.
                return;
            }

            //플레이어 추가하고
            _sessions.Add(session);
            session.Room = this;

            //신입생한테 모든 플레이어 목록 전송
            S_PlayerList players = new S_PlayerList();
            foreach (ClientSession s in _sessions)
            {
                players.players.Add(new S_PlayerList.Player()
                {
                    id = s.SessionId,
                    nickname = s.Player.Nickname,
                    isReady = s.Player.IsReady,
                    character = (int)s.Player.Character
                });
            }
            players.roomId = _roomId;
            session.Send(players.Write());

            //신입생 입장을 모두에게 알린다
            S_EnterPlayer enter = new S_EnterPlayer();
            enter.playerId = session.SessionId;
            enter.nickname = session.Player.Nickname;
            Broadcast(enter.Write());
        }

        public void Leave(ClientSession session)
        {
            //플레이어 제거하고
            _sessions.Remove(session);
            session.Room = null;

            //모두에게 알린다
            //S_BroadcastLeaveGame leave = new S_BroadcastLeaveGame();
            //leave.playerId = session.SessionId;
            //Broadcast(leave.Write());
        }

    }
}
