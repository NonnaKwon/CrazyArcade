using CrazyArcade_Server.Game;
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
        Program.Lobby.SendRoomList(session);
    }

    public static void C_CreateRoomHandler(PacketSession session, IPacket packet)
    {
        C_CreateRoom recvPacket = packet as C_CreateRoom;

        if (recvPacket == null)
            return;

        GameRoom gameRoom = new GameRoom();
        gameRoom.RoomName = recvPacket.roomName;
        gameRoom.MaxPlayer = recvPacket.maxPlayer;
        gameRoom.Enter(session as ClientSession);

        Program.Lobby.CreateRoom(gameRoom);
    }
    public static void C_EnterRoomHandler(PacketSession session, IPacket packet)
    {

    }
    public static void C_LeaveRoomHandler(PacketSession session, IPacket packet)
    {

    }
}
