﻿using GameServer;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static S_RoomList;

namespace CrazyArcade_Server.Game
{
    public class GameLobby : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();
        List<GameRoom> _gameRooms = new List<GameRoom>();
        JobQueue _jobQueue = new JobQueue();
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        public void Flush()
        {
            foreach (GameRoom g in _gameRooms)
                g.Flush();

            foreach (ClientSession s in _sessions)
                s.Send(_pendingList);

            _pendingList.Clear();
        }

        public void Broadcast(ArraySegment<byte> segment)
        {
            _pendingList.Add(segment);
        }

        public void Enter(ClientSession session)
        {
            _sessions.Add(session);
        }

        public void Leave(ClientSession session)
        {
            _sessions.Remove(session);
        }

        public void CreateRoom(GameRoom gameRoom,ClientSession master)
        {
            _gameRooms.Add(gameRoom);

            // 로비에 있는 유저들에게 방이 만들어졌다고 알린다.
            S_CreateRoom sendPacket = new S_CreateRoom();
            sendPacket.roomId = gameRoom.Id;
            sendPacket.masterId = master.SessionId;
            sendPacket.roomName = gameRoom.RoomName;
            sendPacket.maxPlayer = gameRoom.MaxPlayer;

            Broadcast(sendPacket.Write());
        }


        public void SendRoomList(PacketSession session)
        {
            if (_gameRooms.Count == 0)
                return;

            S_RoomList roomListPacket = new S_RoomList();
            foreach (GameRoom room in _gameRooms)
            {
                roomListPacket.rooms.Add(new S_RoomList.Room()
                {
                    id = room.Id,
                    roomName = room.RoomName,
                    map = (int)room.Map,
                    maxPlayer = room.MaxPlayer,
                    playerCount = room.PlayerCount,
                    isStart = room.IsStart
                });
            }

            session.Send(roomListPacket.Write());
        }

        public GameRoom FindRoomById(int id)
        {
            foreach(GameRoom gameRoom in _gameRooms)
            {
                if (gameRoom.Id == id)
                    return gameRoom;
            }

            Console.WriteLine("FindRoomById() : not found room");
            return null;
        }

        public void EnterRoom(ClientSession client, int roomId)
        {
            Leave(client);
            GameRoom enterRoom = FindRoomById(roomId);
            enterRoom.Enter(client);
        }
    }
}
