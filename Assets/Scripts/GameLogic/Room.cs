using System;
using System.Collections.Generic;

public class Room
{
	private List<PlayerSession> sessions;
	public Room ()
	{
		sessions = new List<PlayerSession> ();
	}
}

