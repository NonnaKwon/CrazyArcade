public class GameManager
{
    public GameRoom CurrentRoom { get; set; }
    public MyPlayer Player { get { return _player; } }
    private MyPlayer _player;

    public void LeaveCurrentRoom()
    {
        // 서버에게 나간다는 패킷을 보낸다.
        C_LeaveRoom packet = new C_LeaveRoom();
        packet.roomId = CurrentRoom.Id;
        Managers.Network.Send(packet);

        CurrentRoom = null;
    }

    public void ConnectedPlayer(S_Connect connectInfo)
    {
        _player = new MyPlayer(connectInfo.id);
        _player.Nickname = connectInfo.nickname;
    }

    public void DisConnectedPlayer()
    {
        _player = null;
        LeaveCurrentRoom();
    }
}