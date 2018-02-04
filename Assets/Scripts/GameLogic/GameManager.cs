using System;
using schema;
using UnityEngine;

public class GameManager
{
	public GameManager ()
	{
	}
		
	public void setUserSession(UserSession session) {
		this.session = session;
	}

	public void setNetworkManager(NetworkManager networkManager) {
		this.networkManager = networkManager;
	}

	public void HandleIncomingData(Message msg) {
		Debug.Log("==========Incomming message===============: "  + msg.DataType);
	}

	private UserSession session;
	private NetworkManager networkManager;

}


