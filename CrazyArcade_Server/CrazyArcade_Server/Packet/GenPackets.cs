using System;
using ServerCore;
using System.Collections.Generic;
using System.Net;
using System.Text;

public enum PacketID
{
    C_CreateRoom = 1,
	S_CreateRoom = 2,
	
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}


public class C_CreateRoom : IPacket
{
    public int roomId;
	public string roomName;

	public ushort Protocol { get { return (ushort)PacketID.C_CreateRoom; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
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
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_CreateRoom), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(this.roomId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		ushort roomNameLen = (ushort)Encoding.Unicode.GetBytes(this.roomName, 0, this.roomName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(roomNameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += roomNameLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset + count, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

public class S_CreateRoom : IPacket
{
    public int roomId;
	public string roomName;

	public ushort Protocol { get { return (ushort)PacketID.S_CreateRoom; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        count += sizeof(ushort);
        count += sizeof(ushort);
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
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_CreateRoom), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(this.roomId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		ushort roomNameLen = (ushort)Encoding.Unicode.GetBytes(this.roomName, 0, this.roomName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(roomNameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += roomNameLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset + count, sizeof(ushort));

        return SendBufferHelper.Close(count);

    }
}

