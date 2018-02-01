using System;
using NUnit.Framework;

[TestFixture()]
public class EventDispatcherTest
{
	public class TestHandler: IEventHandler {
		int x = 0;
		public void onEvent(Event evt, Object extra) {
			x = (int)extra;
		}

		public int getX() {
			return x;
		}
	}

	[Test]
	public void EventDispatcherTest_dispatchEvent ()
	{
		EventDispatcher dispatcher = new EventDispatcher ();
		TestHandler handler = new TestHandler ();
		dispatcher.addListener (EventType.JOINED_ROOM_SUCCESS, handler);
		Event evt = new Event (EventType.JOINED_ROOM_SUCCESS);
		dispatcher.dispatchEvent (evt, 1);
		Assert.AreEqual (handler.getX (), 1);

		dispatcher.removeListener (EventType.JOINED_ROOM_SUCCESS, handler);
		dispatcher.dispatchEvent (evt, 2);
		Assert.AreEqual (handler.getX (), 1);
	}
}


