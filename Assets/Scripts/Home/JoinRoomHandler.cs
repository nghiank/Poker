using System;
using schema;
using UnityEngine.SceneManagement;
using UnityEngine;


public class JoinRoomHandler:IEventHandler
{
	private UserSession userSession;
	public JoinRoomHandler (UserSession userSession)
	{
		this.userSession = userSession;
	}

	public void onEvent(Event evt, System.Object e) {
		SceneManager.LoadScene("RoomScene", LoadSceneMode.Single);
		NetworkManager network = (NetworkManager)e;
		GameManager gameManager = ClientContext.Instance.GetUserSession ().GetGameManager ();
		gameManager.setNetworkManager (network);
		Debug.Log ("Starting receiving data..");
		network.Receive (gameManager.HandleIncomingData);
	}
}


