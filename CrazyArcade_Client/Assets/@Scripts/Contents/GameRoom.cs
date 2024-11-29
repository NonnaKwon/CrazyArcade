using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameRoom
{
    public string RoomName { get { return _roomName; } set { _roomName = value; } }
    public int Id { get { return _id; } }
    public Map Map { get { return _map; } set { _map = value; } }
    public int PlayerCount { get { return _playerCount; } }
    public int MaxPlayer { get { return _maxPlayer; } set { _maxPlayer = value; } }
    public bool IsStart { get { return _isStart; } set { _isStart = value; } }

    private int _id;
    private string _roomName;
    private int _playerCount;
    private Map _map;
    private int _maxPlayer;
    private bool _isStart;

    private List<Player> _players = new List<Player>();

    public GameRoom(int roomId)
    {
        _id = roomId;
        _map = Map.Block;
        _isStart = false;
        _roomName = "";
    }
    public GameRoom(int id,string roomName, int map,int maxPlayer,int count,bool isStart)
    {
        _id = id;
        _roomName = roomName;
        _map = (Map)map;
        _maxPlayer = maxPlayer;
        _playerCount = count;
        _isStart = isStart;
    }


    public void UpdatePlayers(S_PlayerList players)
    {
        _players.Clear();

        // 플레이어를 넣는다.
        foreach (S_PlayerList.Player player in players.players)
        {
            Player addPlayer = new Player(player.id);
            addPlayer.Nickname = player.nickname;
            addPlayer.IsReady = player.isReady;
            addPlayer.Character = (Character)player.character;
            _players.Add(addPlayer);
        }

        _playerCount = _players.Count;
    }

    public void Leave()
    {
        _playerCount--;
    }

    public void Enter(Player newPlayer)
    {
        if(newPlayer == Managers.Game.Player)
        {
            Managers.Game.CurrentRoom = this;
            Managers.Scene.LoadScene(Define.Scene.GameRoomScene);
        }

        _players.Add(newPlayer);
        _playerCount++;
    }

    public bool TryEnterRoom()
    {
        if (_playerCount >= MaxPlayer)
            return false;

        C_EnterRoom enterPacket = new C_EnterRoom();
        enterPacket.roomId = _id;
        Managers.Network.Send(enterPacket);
        return true;
    }
}
