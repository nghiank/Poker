using System;

public class Event
{
	private EventType eventType;
	public Event (EventType eventType)
	{
		this.eventType = eventType;
	}

	public EventType GetEventType() {
		return this.eventType;
	}
}

