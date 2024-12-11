using ServerCore;
using System;
using System.Collections.Generic;

public class PacketManager
{
    #region Singleton
    static PacketManager _instance = new PacketManager();
    public static PacketManager Instance { get { return _instance; } }
    #endregion

    PacketManager()
    {
        Register();
    }

    Dictionary<ushort, Func<PacketSession, ArraySegment<byte>,IPacket>> _makeFunc = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();
    Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();

    public void Register()
    {
        
        _makeFunc.Add((ushort)PacketID.S_Connect, MakePacket<S_Connect>);
        _handler.Add((ushort)PacketID.S_Connect, PacketHandler.S_ConnectHandler);
        _makeFunc.Add((ushort)PacketID.S_RoomList, MakePacket<S_RoomList>);
        _handler.Add((ushort)PacketID.S_RoomList, PacketHandler.S_RoomListHandler);
        _makeFunc.Add((ushort)PacketID.S_CreateRoom, MakePacket<S_CreateRoom>);
        _handler.Add((ushort)PacketID.S_CreateRoom, PacketHandler.S_CreateRoomHandler);
        _makeFunc.Add((ushort)PacketID.S_EnterPlayer, MakePacket<S_EnterPlayer>);
        _handler.Add((ushort)PacketID.S_EnterPlayer, PacketHandler.S_EnterPlayerHandler);
        _makeFunc.Add((ushort)PacketID.S_PlayerList, MakePacket<S_PlayerList>);
        _handler.Add((ushort)PacketID.S_PlayerList, PacketHandler.S_PlayerListHandler);
        _makeFunc.Add((ushort)PacketID.S_LeavePlayer, MakePacket<S_LeavePlayer>);
        _handler.Add((ushort)PacketID.S_LeavePlayer, PacketHandler.S_LeavePlayerHandler);
        _makeFunc.Add((ushort)PacketID.S_UpdatePlayerInfo, MakePacket<S_UpdatePlayerInfo>);
        _handler.Add((ushort)PacketID.S_UpdatePlayerInfo, PacketHandler.S_UpdatePlayerInfoHandler);
        _makeFunc.Add((ushort)PacketID.S_UpdateRoomInfo, MakePacket<S_UpdateRoomInfo>);
        _handler.Add((ushort)PacketID.S_UpdateRoomInfo, PacketHandler.S_UpdateRoomInfoHandler);
    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer,Action<PacketSession,IPacket> onRecvCallback = null)
    {
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
        if (_makeFunc.TryGetValue(id, out func))
        {
            IPacket packet = func.Invoke(session, buffer);
            if (onRecvCallback != null)
                onRecvCallback.Invoke(session, packet);
            else
                PacketQueue.Instance.Push(packet);
        }
    }

    T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket,new() //new도 가능해야한다. 라는 조건
    {
        T pkt = new T();
        pkt.Read(buffer);
        return pkt;
    }

    public void HandlePacket(PacketSession session,IPacket packet)
    {
        Action<PacketSession, IPacket> action = null;
        if (_handler.TryGetValue(packet.Protocol, out action))
            action.Invoke(session, packet);
    }
}