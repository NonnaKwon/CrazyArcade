using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PacketHandler
{
    public static void S_RoomListHandler(PacketSession session, IPacket packet)
    {
        // 패킷이 들어온다.
        // 로비씬 클래스의 룸 리스트만 바꿔준다.
    }
    
    public static void S_CreateRoomHandler(PacketSession session, IPacket packet)
    {

    }
    public static void S_EnterPlyerHandler(PacketSession session, IPacket packet)
    {

    }
    public static void S_PlayerListHandler(PacketSession session, IPacket packet)
    {

    }
    public static void S_LeavePlyerHandler(PacketSession session, IPacket packet)
    {

    }
}
