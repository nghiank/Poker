using System;

public class UserSession
{
	private static UserSession instance;

	private UserSession() {}

	public static UserSession Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new UserSession();
			}
			return instance;
		}
	}

	public void setAuthToken(string authToken) {
		this.authToken = authToken;
	}

	public string getAuthToken() {
		return this.authToken;
	}

	public void setUserInfo(UserInfo userInfo) {
		this.userInfo = userInfo;
	}

	public UserInfo getUserInfo() {
		return this.userInfo;
	}

	private string authToken;
	private UserInfo userInfo;
}