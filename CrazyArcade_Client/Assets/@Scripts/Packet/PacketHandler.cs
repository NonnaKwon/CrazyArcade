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
        LobbyScene lobby = FindLobbyScene();

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
        //TODO : 로비에 만들어진 Room을 넣기. -> UI 동기화하기.
        LobbyScene lobby = FindLobbyScene();
        S_CreateRoom createRoom = packet as S_CreateRoom;

        GameRoom room = new GameRoom(createRoom.roomId);
        room.RoomName = createRoom.roomName;
        room.MaxPlayer = createRoom.maxPlayer;
        lobby.AddRoom(room);

        if(Managers.Game.Player.Id == createRoom.masterId)
            room.TryEnterRoom();
    }

    public static void S_EnterPlayerHandler(PacketSession session, IPacket packet)
    {

    }

    public static void S_PlayerListHandler(PacketSession session, IPacket packet)
    {
        LobbyScene lobby = FindLobbyScene();

        // 방에 들어간다.
        S_PlayerList roomPlayer = packet as S_PlayerList;
        GameRoom enterRoom = lobby.FindRoomById(roomPlayer.roomId);

        enterRoom.UpdatePlayers(roomPlayer);
        enterRoom.Enter(Managers.Game.Player);
    }

    public static void S_LeavePlyerHandler(PacketSession session, IPacket packet)
    {

    }

    private static LobbyScene FindLobbyScene()
    {
        LobbyScene lobby = Managers.Scene.CurrentScene as LobbyScene;
        if (lobby == null)
        {
            Debug.LogError("not find lobby");
            return null;
        }

        return lobby;
    }
}
