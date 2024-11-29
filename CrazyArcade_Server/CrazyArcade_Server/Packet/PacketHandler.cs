using CrazyArcade_Server.Game;
using GameServer;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static S_RoomList;

class PacketHandler
{
    public static void C_EnterLobbyHandler(PacketSession session, IPacket packet)
    {
        Program.Lobby.SendRoomList(session);
    }

    public static void C_CreateRoomHandler(PacketSession session, IPacket packet)
    {
        C_CreateRoom recvPacket = packet as C_CreateRoom;
        ClientSession client = session as ClientSession;

        GameRoom gameRoom = new GameRoom();
        gameRoom.RoomName = recvPacket.roomName;
        gameRoom.MaxPlayer = recvPacket.maxPlayer;

        Program.Lobby.CreateRoom(gameRoom, client);
    }

    public static void C_EnterRoomHandler(PacketSession session, IPacket packet)
    {
        ClientSession client = session as ClientSession;
        C_EnterRoom room = packet as C_EnterRoom;
        Program.Lobby.EnterRoom(client, room.roomId);
    }

    public static void C_LeaveRoomHandler(PacketSession session, IPacket packet)
    {

    }
}
