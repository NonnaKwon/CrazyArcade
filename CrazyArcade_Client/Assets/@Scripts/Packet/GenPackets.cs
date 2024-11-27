using System;
using ServerCore;
using System.Collections.Generic;
using System.Net;
using System.Text;

public enum PacketID
{
    C_EnterLobby = 1,
	S_RoomList = 2,
	C_CreateRoom = 3,
	S_CreateRoom = 4,
	C_EnterRoom = 5,
	S_EnterPlyer = 6,
	S_PlayerList = 7,
	C_LeaveRoom = 8,
	S_LeavePlyer = 9,
	
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}


public class C_EnterLobby : IPacket
{
    

	public ushort Protocol { get { return (ushort)PacketID.C_EnterLobby; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_EnterLobby), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class S_RoomList : IPacket
{
    
	public class Room
	{
	    public int id;
		public string roomName;
		public int map;
		public int playerCount;
		public bool isStart;
	
	    public void Read(ArraySegment<byte> segment, ref ushort count)
	    {
	        this.id = BitConverter.ToInt32(segment.Array, segment.Offset + count);
			count += sizeof(int);
			ushort roomNameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
			count += sizeof(ushort);
			this.roomName = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,roomNameLen);
			count += roomNameLen;
			this.map = BitConverter.ToInt32(segment.Array, segment.Offset + count);
			count += sizeof(int);
			this.playerCount = BitConverter.ToInt32(segment.Array, segment.Offset + count);
			count += sizeof(int);
			this.isStart = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
			count += sizeof(bool);
	    }
	
	    public bool Write(ArraySegment<byte> segment,ref ushort count)
	    {
	        bool success = true;
	        Array.Copy(BitConverter.GetBytes(this.id), 0, segment.Array, segment.Offset + count, sizeof(int));
			count += sizeof(int);
			ushort roomNameLen = (ushort)Encoding.Unicode.GetBytes(this.roomName, 0, this.roomName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
			Array.Copy(BitConverter.GetBytes(roomNameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
			count += sizeof(ushort);
			count += roomNameLen;
			Array.Copy(BitConverter.GetBytes(this.map), 0, segment.Array, segment.Offset + count, sizeof(int));
			count += sizeof(int);
			Array.Copy(BitConverter.GetBytes(this.playerCount), 0, segment.Array, segment.Offset + count, sizeof(int));
			count += sizeof(int);
			Array.Copy(BitConverter.GetBytes(this.isStart), 0, segment.Array, segment.Offset + count, sizeof(bool));
			count += sizeof(bool);
	        return success;
	    }
	}
	
	public List<Room> rooms = new List<Room>();

	public ushort Protocol { get { return (ushort)PacketID.S_RoomList; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        
		this.rooms.Clear();
		ushort roomLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for(int i=0;i<roomLen;i++)
		{
		    Room room = new Room();
		    room.Read(segment, ref count);
		    rooms.Add(room);
		}
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_RoomList), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes((ushort)this.rooms.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach(Room room in this.rooms)
		    room.Write(segment, ref count);
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class C_CreateRoom : IPacket
{
    public string roomName;
	public int maxPlayer;

	public ushort Protocol { get { return (ushort)PacketID.C_CreateRoom; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        ushort roomNameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.roomName = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,roomNameLen);
		count += roomNameLen;
		this.maxPlayer = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_CreateRoom), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort roomNameLen = (ushort)Encoding.Unicode.GetBytes(this.roomName, 0, this.roomName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(roomNameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += roomNameLen;
		Array.Copy(BitConverter.GetBytes(this.maxPlayer), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class S_CreateRoom : IPacket
{
    public string roomName;
	public int maxPlayer;

	public ushort Protocol { get { return (ushort)PacketID.S_CreateRoom; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        ushort roomNameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.roomName = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,roomNameLen);
		count += roomNameLen;
		this.maxPlayer = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_CreateRoom), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort roomNameLen = (ushort)Encoding.Unicode.GetBytes(this.roomName, 0, this.roomName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(roomNameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += roomNameLen;
		Array.Copy(BitConverter.GetBytes(this.maxPlayer), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class C_EnterRoom : IPacket
{
    public int roomId;

	public ushort Protocol { get { return (ushort)PacketID.C_EnterRoom; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        this.roomId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_EnterRoom), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(this.roomId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class S_EnterPlyer : IPacket
{
    public int roomId;
	public string playerId;

	public ushort Protocol { get { return (ushort)PacketID.S_EnterPlyer; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        this.roomId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		ushort playerIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.playerId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,playerIdLen);
		count += playerIdLen;
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_EnterPlyer), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(this.roomId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		ushort playerIdLen = (ushort)Encoding.Unicode.GetBytes(this.playerId, 0, this.playerId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(playerIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += playerIdLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class S_PlayerList : IPacket
{
    
	public class Player
	{
	    public string id;
		public string nickname;
		public bool isReady;
		public int character;
	
	    public void Read(ArraySegment<byte> segment, ref ushort count)
	    {
	        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
			count += sizeof(ushort);
			this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,idLen);
			count += idLen;
			ushort nicknameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
			count += sizeof(ushort);
			this.nickname = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,nicknameLen);
			count += nicknameLen;
			this.isReady = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
			count += sizeof(bool);
			this.character = BitConverter.ToInt32(segment.Array, segment.Offset + count);
			count += sizeof(int);
	    }
	
	    public bool Write(ArraySegment<byte> segment,ref ushort count)
	    {
	        bool success = true;
	        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
			Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
			count += sizeof(ushort);
			count += idLen;
			ushort nicknameLen = (ushort)Encoding.Unicode.GetBytes(this.nickname, 0, this.nickname.Length, segment.Array, segment.Offset + count + sizeof(ushort));
			Array.Copy(BitConverter.GetBytes(nicknameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
			count += sizeof(ushort);
			count += nicknameLen;
			Array.Copy(BitConverter.GetBytes(this.isReady), 0, segment.Array, segment.Offset + count, sizeof(bool));
			count += sizeof(bool);
			Array.Copy(BitConverter.GetBytes(this.character), 0, segment.Array, segment.Offset + count, sizeof(int));
			count += sizeof(int);
	        return success;
	    }
	}
	
	public List<Player> players = new List<Player>();
	public int roomId;
	public string roomName;

	public ushort Protocol { get { return (ushort)PacketID.S_PlayerList; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        
		this.players.Clear();
		ushort playerLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for(int i=0;i<playerLen;i++)
		{
		    Player player = new Player();
		    player.Read(segment, ref count);
		    players.Add(player);
		}
		this.roomId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		ushort roomNameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.roomName = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,roomNameLen);
		count += roomNameLen;
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_PlayerList), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes((ushort)this.players.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach(Player player in this.players)
		    player.Write(segment, ref count);
		Array.Copy(BitConverter.GetBytes(this.roomId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		ushort roomNameLen = (ushort)Encoding.Unicode.GetBytes(this.roomName, 0, this.roomName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(roomNameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += roomNameLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class C_LeaveRoom : IPacket
{
    public int roomId;

	public ushort Protocol { get { return (ushort)PacketID.C_LeaveRoom; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        this.roomId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_LeaveRoom), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(this.roomId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class S_LeavePlyer : IPacket
{
    public int roomId;
	public string playerId;

	public ushort Protocol { get { return (ushort)PacketID.S_LeavePlyer; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
        this.roomId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		ushort playerIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.playerId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count,playerIdLen);
		count += playerIdLen;
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);

        ushort count = 0;
        bool success = true;

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_LeavePlyer), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(this.roomId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		ushort playerIdLen = (ushort)Encoding.Unicode.GetBytes(this.playerId, 0, this.playerId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(playerIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += playerIdLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

