using System;
using schema;

public class JoinRoomHandler:IEventHandler
{
	private UserSession userSession;
	public JoinRoomHandler (UserSession userSession)
	{
		this.userSession = userSession;
	}

	public void onEvent(Event evt, Object e) {

		FlatBuffers.ByteBuffer buf = new FlatBuffers.ByteBuffer ((byte[])e);
		Message msg = Message.GetRootAsMessage(buf);
		if (msg.DataType == Data.ReconnectKey) {	
			ReconnectKey reconnectKey = (ReconnectKey)(msg.Data<ReconnectKey>());
			userSession.SetReconnectKey (reconnectKey);
		}
	}
}


