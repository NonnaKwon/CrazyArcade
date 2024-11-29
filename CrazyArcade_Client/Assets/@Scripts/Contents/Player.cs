using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static S_RoomList;

public class Player : MonoBehaviour
{
    public int Id { get { return _id; } }
    public string Nickname { get { return _nickname; } set { _nickname = value; } }
    public bool IsReady { get { return _isReady; } set { _isReady = value; } }
    public Character Character { get { return _character; } set { _character = value; } }
    
    private int _id;
    private string _nickname;
    private bool _isReady;
    private Character _character;

    public Player(int id)
    {
        _id = id;
        Init();
    }

    private void Init()
    {
        _isReady = false;
        _character = Character.Bazzi;
    }

    #region 연산자 오버로딩
    public static bool operator ==(Player player1,Player player2)
    {
        return player1._id == player2._id;
    }

    public static bool operator !=(Player player1, Player player2)
    {
        return !(player1 == player2);
    }

    public override bool Equals(object other)
    {
        Player player = other as Player;
        if ( player == null)
            return false;
        return player == this;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id);
    }
    #endregion
}
