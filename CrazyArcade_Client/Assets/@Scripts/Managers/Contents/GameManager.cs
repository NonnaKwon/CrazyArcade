public class GameManager
{
    public GameRoom CurrentRoom { get; set; }
    public MyPlayer Player { get { return _player; } }
    private MyPlayer _player;

    public void ConnectedPlayer(S_Connect connectInfo)
    {
        _player = new MyPlayer(connectInfo.id);
        _player.Nickname = connectInfo.nickname;
    }

    public void DisConnectedPlayer()
    {
        _player = null;
    }
}