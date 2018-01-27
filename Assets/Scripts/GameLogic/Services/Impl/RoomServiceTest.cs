using System;
using NUnit.Framework;

[TestFixture()]
public class RoomServiceTest
{
	[Test]
	public void FindCorrectRoom() 
	{
		IRoomService service = new RoomService ();
		Assert.AreEqual (service.findRoom (), "Singapore");
	}
}


