using System;
using UnityEngine;
using static Define;

public class GameManager
{
    void OnApplicationQuit()
    {
        Managers.Network.GameServer.Disconnect();
    }
}