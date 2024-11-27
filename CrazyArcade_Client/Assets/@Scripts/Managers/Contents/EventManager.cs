using System;
using System.Collections.Generic;
using static Define;

public class EventManager
{
	private Dictionary<EventType, Action> _events = new Dictionary<EventType, Action>();

	public void AddEvent(EventType eventType, Action listener)
	{
		if (_events.ContainsKey(eventType) == false)
			_events.Add(eventType, new Action(() => { }));

		_events[eventType] += listener;
	}

	public void RemoveEvent(EventType eventType, Action listener)
	{
        if (_events.ContainsKey(eventType))
			_events[eventType] -= listener;
	}

	public void TriggerEvent(EventType eventType)
	{
		if (_events.ContainsKey(eventType))
			_events[eventType].Invoke();
	}

	public void Clear()
	{
		_events.Clear();
	}

}
