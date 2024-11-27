using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class ServerInstance
{
    ServerSession _session = null;
    Connector _connector = new Connector();

    public bool IsConnected()
    {
        if (_session == null)
            return false;
        return _session.IsConnected();
    }

    public void Update()
    {
        if (_session == null)
            return;

    }

    public void Send(IPacket packet)
    {
        if (_session != null)
            _session.Send(packet);
    }

    public void Connect(IPEndPoint endPoint)
    {
        _session = new ServerSession();
        _connector.Connect(endPoint, () => { return _session; });
    }

    public void Disconnect()
    {
        if(_session != null)
            _session.Disconnect();
        _session = null;
    }
}

public class NetworkManager
{
    public ServerInstance GameServer { get; } = new ServerInstance();

    private const int SERVER_PORT = 7777;

    public void Init()
    {
        if (GameServer.IsConnected())
            return;

        IPAddress[] ipAddr = Dns.GetHostAddresses(Dns.GetHostName());
        IPEndPoint endPoint = new IPEndPoint(ipAddr[0], SERVER_PORT);
        GameServer.Connect(endPoint);
    }

    public void Update()
    {
        GameServer.Update();
    }

    public void Send(IPacket packet)
    {
        GameServer.Send(packet);
    }

    public void DisConnect()
    {
        GameServer.Disconnect();
    }

}
