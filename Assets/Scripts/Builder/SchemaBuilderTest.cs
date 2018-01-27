using System;
using NUnit.Framework;
using FlatBuffers;
using schema;

[TestFixture()]
public class SchemaBuilderTest
{
	[Test]
	public void buildJoinRoom_test() 
	{
		string roomId = "Singapore";
		string authToken = "abcde";
		FlatBufferBuilder builder = SchemaBuilder.buildJoinRoom(roomId, authToken);
		byte[] res = builder.SizedByteArray();
		ByteBuffer byteBuf = new ByteBuffer (res);
		Message msg = Message.GetRootAsMessage (byteBuf);
		JoinRoomCommand cmd = (JoinRoomCommand)(msg.Data<JoinRoomCommand>());
		Assert.AreEqual (msg.DataType, Data.JoinRoomCommand);
		Assert.AreEqual (cmd.RoomId, roomId);
		Assert.AreEqual (cmd.Token, authToken);
	}

	[Test]
	public void buildByte_test() {
		string roomId = "a";
		string authToken = "b";
		FlatBufferBuilder builder = SchemaBuilder.buildJoinRoom(roomId, authToken);
		byte[] b = SchemaBuilder.buildPrependedLength (builder);
		byte[] c = builder.SizedByteArray ();

		Assert.AreEqual ((b [0] << 8) + b[1], c.Length);
		for (int i = 0; i < c.Length; ++i) {
			Assert.AreEqual (b [i + 2], c [i]);
		}
	}
}


