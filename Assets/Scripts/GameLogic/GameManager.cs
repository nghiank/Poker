using System;
using schema;
using UnityEngine;

public class GameManager
{
	public GameManager ()
	{
	}
		
	public void setNetworkManager(NetworkManager networkManager) {
		this.networkManager = networkManager;
	}

	public void HandleIncomingData(Message msg) {
		Debug.Log("==========Incomming message===============: "  + msg.DataType);
		switch (msg.DataType) {
		case Data.RoomInfo:
			SetupRoomForClient (msg.Data<RoomInfo> ().Value);
			networkManager.Receive (this.HandleIncomingData);
			break;
		}
	}

	private void SetupRoomForClient(RoomInfo room) {
		for (int i = 0; i < room.PlayersLength; ++i) {
			Debug.Log ("=====Player " + i + " =============");
			PlayerInfo info = (PlayerInfo)room.Players (i);
			Debug.Log (info.UserId.ToString());
			Debug.Log (info.Name.ToString());
		}
	}
		
	private NetworkManager networkManager;

}


