using System;
using UnityEngine;


public class JoinCommand: ICommand
{
	private string roomId;
	private string authToken;
	public JoinCommand (string roomId, string authToken)
	{
		this.roomId = roomId;
		this.authToken = authToken;
	}

	public void perform(EventDispatcher dispatcher) {
		string hostId = "127.0.0.1";
		int port = 8080;
		NetworkManager network = new NetworkManager (hostId, port);
		network.Connect (null);
		network.getConnectDoneEvent ().WaitOne ();

		var joinRoom = SchemaBuilder.buildJoinRoom("Singapore", ClientContext.Instance.GetUserSession().GetAuthToken());

		Debug.Log("Join room in progress...");
		network.Send(SchemaBuilder.buildPrependedLength(joinRoom), null);
		network.getSendDoneEvent().WaitOne();
		Debug.Log("Join room sent done!");
		network.Receive (null);
		network.getReceiveDoneEvent ().WaitOne (Constants.TIME_OUT_MS);

		if (network.getDecoder ().getState () == FlatBuffersDecoder.ReadState.DONE) {
			Event success = new Event (EventType.JOINED_ROOM_SUCCESS);
			dispatcher.dispatchEvent (success, network.getDecoder ().getData ());
		} else {
			Event failed = new Event (EventType.JOINED_ROOM_FAILED);
			dispatcher.dispatchEvent (failed, null);
		} 
	}
}


