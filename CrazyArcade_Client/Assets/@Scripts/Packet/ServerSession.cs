using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerSession : PacketSession
{
    public override void OnConnected(EndPoint endPoint)
    {
        Debug.LogError($"OnConnected");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Debug.Log($"OnDisconnected");
    }

    public override void OnRecvPacket(ArraySegment<byte> buffer)
    {
        Debug.Log($"OnRecvPacket");
    }

    public override void OnSend(int numOfBytes)
    {
        Debug.Log($"OnSend");
    }
}
