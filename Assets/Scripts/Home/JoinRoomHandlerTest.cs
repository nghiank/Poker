using System;
using NUnit.Framework;


public class FakeUserSession: UserSession {
	private string key;
}

[TestFixture()]
public class JoinRoomHandlerTest
{
	[Test]
	public void JoinRoomHandlerTest_testValidOnEvent ()
	{
		FakeUserSession fakeUserSession = new FakeUserSession ();
		JoinRoomHandler handler = new JoinRoomHandler (fakeUserSession);
		byte[] e = SchemaBuilder.buildReconnectKey ("helloworld").SizedByteArray();
		handler.onEvent (new Event(EventType.JOINED_ROOM_SUCCESS), e);
		Assert.AreEqual (fakeUserSession.GetReconnectKey ().Key , "helloworld");
	}

	[Test]
	public void JoinRoomHandlerTest_testInValidOnEvent ()
	{
		FakeUserSession fakeUserSession = new FakeUserSession ();
		JoinRoomHandler handler = new JoinRoomHandler (fakeUserSession);
		byte[] e = SchemaBuilder.buildJoinRoom("ad", "ef").SizedByteArray();
		handler.onEvent (new Event(EventType.JOINED_ROOM_SUCCESS), e);
	}
}

