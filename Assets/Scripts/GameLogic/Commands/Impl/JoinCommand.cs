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

	private void signalJoinFailed(EventDispatcher dispatcher) {
		Event failed = new Event (EventType.JOINED_ROOM_FAILED);
		dispatcher.dispatchEvent (failed, null);
	}

	public void perform(EventDispatcher dispatcher) {
		string hostId = "127.0.0.1";
		int port = 8080;
		NetworkManager network = new NetworkManager (hostId, port);
		Debug.Log("Join room connect!");
		network.Connect (null);
		if (!network.getConnectDoneEvent ().WaitOne (Constants.TIME_OUT_MS)) {
			signalJoinFailed (dispatcher);
			return;
		}
		Debug.Log("Connecting to room!");

		var joinRoom = SchemaBuilder.buildJoinRoom("Singapore", ClientContext.Instance.GetUserSession().GetAuthToken());
		Debug.Log("Join room in progress...");
		network.Send(SchemaBuilder.buildPrependedLength(joinRoom), null);
		if (!network.getSendDoneEvent ().WaitOne (Constants.TIME_OUT_MS)) {
			signalJoinFailed (dispatcher);
			return;
		}
		Debug.Log("Join room sent done!");
		Event success = new Event (EventType.JOINED_ROOM_SUCCESS);
		dispatcher.dispatchEvent (success, network);
	}
}


