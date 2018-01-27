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
		NetworkManager network = new NetworkManager ();
	}
}


