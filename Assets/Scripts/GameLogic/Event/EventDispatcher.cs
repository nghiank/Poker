using System;
using System.Collections.Generic;
using UnityEngine;


public class EventDispatcher
{
	private Dictionary<EventType, List<IEventHandler> > eventHandlers; 
	public EventDispatcher ()
	{
		this.eventHandlers = new Dictionary<EventType, List<IEventHandler>> ();
	}

	public void addListener(EventType evt, IEventHandler handler) {
		if (eventHandlers.ContainsKey (evt)) {
			eventHandlers [evt].Add (handler);
		} else {
			var list = new List<IEventHandler> ();
			list.Add (handler);
			eventHandlers [evt] = list;
		}
	}

	public void removeListener(EventType evt, IEventHandler handler) {
		Debug.Assert (eventHandlers.ContainsKey (evt));
		eventHandlers [evt].Remove (handler); 
	}

	public void dispatchEvent(Event evt, System.Object extra) {
		EventType type = evt.GetEventType ();
		if (!eventHandlers.ContainsKey (type)) {
			return;
		}
		foreach (IEventHandler handler in eventHandlers[type]) {
			handler.onEvent (evt, extra);
		}
	}
}


