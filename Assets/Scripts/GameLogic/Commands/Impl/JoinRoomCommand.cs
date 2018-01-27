using System;

public class JoinRoomCommand: ICommand
{
	private string roomName;
	public JoinRoomCommand (string roomName)
	{
		this.roomName = roomName;
	}

	public void perform() {
		
	}
}


