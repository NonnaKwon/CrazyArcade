using GameServer;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PacketHandler
{
    public static void C_EnterLobbyHandler(PacketSession session, IPacket packet)
    {
        S_RoomList roomListPkt = new S_RoomList();
        // 방 리스트를 보낸다.
    }

    public static void C_CreateRoomHandler(PacketSession session, IPacket packet)
    {

    }
    public static void C_EnterRoomHandler(PacketSession session, IPacket packet)
    {

    }
    public static void C_LeaveRoomHandler(PacketSession session, IPacket packet)
    {

    }
}
