using System;

public class JoinCommand: ICommand
{
	private string roomId;
	private string authToken;
	public JoinCommand (string roomId, string authToken)
	{
		this.roomId = roomId;
		this.authToken = authToken;
	}

	public void perform() {
		string hostId = "127.0.0.1";
		int port = 8080;
		NetworkManager network = new NetworkManager (hostId, port);
	}
}


