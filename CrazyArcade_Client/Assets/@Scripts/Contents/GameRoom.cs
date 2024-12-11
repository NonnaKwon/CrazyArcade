using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameRoom
{
    public int Id { get { return _id; } }
    public string RoomName { get { return _roomName; } }
    public Map Map { get { return _map; } }
    public int PlayerCount { get { return _playerCount; } }
    public int MaxPlayer { get { return _maxPlayer; } }
    public bool IsStart { get { return _isStart; } }

    private int _id;
    private string _roomName;
    private int _playerCount;
    private Map _map;
    private int _maxPlayer;
    private bool _isStart;

    private List<Player> _players = new List<Player>();

    public GameRoom(int roomId,string roomName,int maxPlayer)
    {
        _id = roomId;
        _roomName = roomName;
        _maxPlayer = maxPlayer;
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

    public void UpdateRoomInfo()
    {
        // TODO : 패킷을 보낸다.
        // TODO : 서버에서 패킷이 되돌아오면, 게임룸UI, 로비씬UI 처리함.
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

    public void Leave(int playerId)
    {
        foreach(Player player in _players)
        {
            if(player.Id == playerId)
            {
                _players.Remove(player);
                break;
            }
        }
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
