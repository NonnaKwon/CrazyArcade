using GameServer;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PacketHandler
{
    public static void C_LeaveGameHandler(PacketSession session,IPacket packet)
    {
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
            return;

        //주문서에 적는것.
        GameRoom room = clientSession.Room;
        room.Push(
            () => room.Leave(clientSession)
        );
    }

    public static void C_MoveHandler(PacketSession session, IPacket packet)
    {
        C_Move movePacket = packet as C_Move;
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
            return;

       //Console.WriteLine($"{clientSession.SessionId} : {movePacket.posX} {movePacket.posY} {movePacket.posZ}");
        
        //주문서에 적는것.
        GameRoom room = clientSession.Room;
        room.Push(
            () => room.Move(clientSession,movePacket)
        );
    }

    public static void C_CreateRoomHandler(PacketSession session, IPacket packet)
    {

    }
}
