﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoom : MonoBehaviour {

	public void JoinRoomClick() {
		Debug.Log ("User Display Name: " + UserSession.Instance.getUserInfo ().getDisplayName ());
		Debug.Log ("User Id : " + UserSession.Instance.getUserInfo ().getUserId ());
		Debug.Log ("Auth token Id : " + UserSession.Instance.getAuthToken ());


		// TODO: Build Command from builder
		IRoomService roomService = new RoomService();
		ICommand command = new JoinCommand (roomService.findRoom(), UserSession.Instance.getAuthToken());
		command.perform ();
	}

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
