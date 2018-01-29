using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoom : MonoBehaviour {


	public void JoinRoomClick() {
		Debug.Log ("User Display Name: " + ClientContext.Instance.GetUserSession().GetUserInfo ().GetDisplayName ());
		Debug.Log ("User Id : " + ClientContext.Instance.GetUserSession().GetUserInfo().GetUserId ());
		Debug.Log ("Auth token Id : " + ClientContext.Instance.GetUserSession().GetAuthToken ());


		// TODO: Build Command from builder
		IRoomService roomService = new RoomService();
		ICommand command = new JoinCommand (roomService.findRoom(), ClientContext.Instance.GetUserSession().GetAuthToken());

		EventDispatcher dispatcher = new EventDispatcher ();
		dispatcher.addListener (EventType.JOINED_ROOM_SUCCESS, new JoinRoomHandler(ClientContext.Instance.GetUserSession()));
		dispatcher.addListener (EventType.JOINED_ROOM_SUCCESS, new SwitchSceneHandler ("RoomScene"));
		command.perform (dispatcher);
	}

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
