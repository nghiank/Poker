using System;

public class ClientContext
{
	private static ClientContext instance;

	private ClientContext() {}

	public static ClientContext Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new ClientContext();
			}
			return instance;
		}
	}

	public UserSession GetUserSession() {
		return userSession;
	}

	public void SetUserSession(UserSession session) {
		this.userSession = session;
	}

	private UserSession userSession;
}


