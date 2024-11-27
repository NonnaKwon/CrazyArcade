using ServerCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static S_RoomList;

class PacketHandler
{
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
        //TODO : 답변이 오면 (S_CreateRoom) 방을 만들고 방에 입장한다. (씬 이동)
        //TODO : 로비에 들어가 있는 아이들한테만 방이 만들어졌다는 패킷을 쏴야한다.
        Debug.LogError("ss");
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
