using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static S_RoomList;

public class GameRoom
{
    private int _id;
    private string _roomName;
    private int _map;
    private int _playerCount;
    private bool _isStart;

    public GameRoom() { }
    public GameRoom(int id,string roomName,int map,int count,bool isStart)
    {
        _id = id;
        _roomName = roomName;
        _map = map;
        _playerCount = count;
        _isStart = isStart;
    }
}
