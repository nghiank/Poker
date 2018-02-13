using System;
using schema;

public class UserSession
{
	public UserSession() {
	}

	public void clearSession() {
		this.authToken = "";
		this.userInfo = null;
	}

	public void SetAuthToken(string authToken) {
		this.authToken = authToken;
	}

	public string GetAuthToken() {
		return this.authToken;
	}

	public void SetUserInfo(UserInfo userInfo) {
		this.userInfo = userInfo;
	}

	public UserInfo GetUserInfo() {
		return this.userInfo;
	}

	public GameManager GetGameManager() {
		return manager;
	}

	public void SetGameManager(GameManager manager) {
		this.manager = manager;
	}

	private string authToken;
	private UserInfo userInfo;
	private GameManager manager;
}