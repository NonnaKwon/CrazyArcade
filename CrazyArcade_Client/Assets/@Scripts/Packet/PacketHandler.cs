using ServerCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static S_RoomList;

class PacketHandler
{
    public static void S_ConnectHandler(PacketSession session, IPacket packet)
    {
        S_Connect connectInfo = packet as S_Connect;
        Managers.Game.ConnectedPlayer(connectInfo);
    }

    public static void S_RoomListHandler(PacketSession session, IPacket packet)
    {
        LobbyScene lobby = Managers.Scene.CurrentScene as LobbyScene;
        if(lobby == null)
        {
            Debug.LogError("not find lobby");
            return;
        }

        S_RoomList roomList = packet as S_RoomList;
        List<GameRoom> gameRooms = new List<GameRoom>();
        foreach(Room room in roomList.rooms)
        {
            gameRooms.Add(new GameRoom(
                room.id,room.roomName,room.map,room.playerCount,room.isStart
            ));
        }

        lobby.GameRooms = gameRooms;
    }
    
    public static void S_CreateRoomHandler(PacketSession session, IPacket packet)
    {
        //TODO : �κ� �� �ִ� ���̵����׸� ���� ��������ٴ� ��Ŷ�� �����Ѵ�.
        Debug.LogError("Create Room");
    }

    public static void S_EnterPlayerHandler(PacketSession session, IPacket packet)
    {

    }

    public static void S_PlayerListHandler(PacketSession session, IPacket packet)
    {
        LobbyScene lobby = Managers.Scene.CurrentScene as LobbyScene;
        if (lobby == null)
        {
            Debug.LogError("not find lobby");
            return;
        }

        // �濡 ����.
        S_PlayerList roomPlayer = packet as S_PlayerList;
        GameRoom enterRoom = lobby.FindRoomById(roomPlayer.roomId);

        enterRoom.UpdatePlayers(roomPlayer);
        enterRoom.Enter(Managers.Game.Player);
    }

    public static void S_LeavePlyerHandler(PacketSession session, IPacket packet)
    {

    }
}
