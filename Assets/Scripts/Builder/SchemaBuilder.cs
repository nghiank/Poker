using System;
using FlatBuffers;
using schema;

public class SchemaBuilder
{
	// Build
	public static byte[] buildPrependedLength(FlatBufferBuilder builder) {
		ByteBuffer buf = builder.DataBuffer;
		int len = buf.Length - buf.Position;
		byte[] combined = new byte[len + 2];
		combined [0] = (byte)((0xFF00 & len) >> 8);
		combined [1] = (byte)(0x00FF & len);
		Buffer.BlockCopy(buf.Data, buf.Position, combined, 2, len);
		return combined;
	}
		
	public static FlatBufferBuilder buildJoinRoom(string roomId, string authToken) {
		var builder = new FlatBufferBuilder(1);
		var authTokenStr = builder.CreateString(authToken);
		var roomIdStr = builder.CreateString(roomId);
		JoinRoomCommand.StartJoinRoomCommand(builder);
		JoinRoomCommand.AddToken(builder, authTokenStr);
		JoinRoomCommand.AddRoomId (builder, roomIdStr);
		var joinCmd = JoinRoomCommand.EndJoinRoomCommand(builder);
		Message.StartMessage(builder);
		Message.AddDataType(builder, schema.Data.JoinRoomCommand);
		Message.AddData(builder, joinCmd.Value);
		var data = Message.EndMessage(builder);
		builder.Finish(data.Value);
		return builder;
	}

	public static FlatBufferBuilder buildReconnectKey(string key) {
		var builder = new FlatBufferBuilder(1);
		var keyStr = builder.CreateString(key);
		ReconnectKey.StartReconnectKey(builder);
		ReconnectKey.AddKey(builder, keyStr);
		var reconnectKey = ReconnectKey.EndReconnectKey(builder);
		Message.StartMessage(builder);
		Message.AddDataType(builder, schema.Data.ReconnectKey);
		Message.AddData(builder, reconnectKey.Value);
		var data = Message.EndMessage(builder);
		builder.Finish(data.Value);
		return builder;
	}

}


