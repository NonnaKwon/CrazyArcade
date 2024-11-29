using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketQueue
{
    public static PacketQueue Instance { get; } = new PacketQueue();
    private Queue<IPacket> _queue = new Queue<IPacket>();
    private object _lock = new object();


    public List<IPacket> PopAll()
    {
        lock(_lock)
        {
            List<IPacket> list = new List<IPacket>();

            while (_queue.Count > 0)
            {
                IPacket packet = _queue.Dequeue();
                list.Add(packet);
            }

            return list;
        }
    }

    public void Push(IPacket packet)
    {
        lock(_lock)
        {
            _queue.Enqueue(packet);
        }
    }

    public IPacket Pop()
    {
        lock(_lock)
        {
            if (_queue.Count == 0)
                return null;
            return _queue.Dequeue();
        }
    }
}
