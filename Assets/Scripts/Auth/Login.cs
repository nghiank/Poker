﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using FlatBuffers;
using schema;
using System;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public GameObject email;
	public GameObject password;

	private string emailStr;
	private string passwordStr;

	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;
	private bool socketReady;

	public void LoginClick() {
		Firebase.Auth.Credential credential =
			Firebase.Auth.EmailAuthProvider.GetCredential(emailStr, passwordStr);
		Firebase.Auth.FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("SignInWithCredentialAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
				return;
			}
			Firebase.Auth.FirebaseUser newUser = task.Result;
			Console.WriteLine ("newUser={0}", newUser);
		
			newUser.TokenAsync(true).ContinueWith(idTokenTask => {
				if (idTokenTask.IsCanceled) {
					Debug.LogError("TokenAsync was canceled.");
					return;
				}
				if (idTokenTask.IsFaulted) {
					Debug.LogError("TokenAsync encountered an error: " + idTokenTask.Exception);
					return;
				}

				string idToken = idTokenTask.Result;
				UserInfo userInfo = new UserInfo(new UserInfo.UserInfoBuilder().DisplayName(newUser.DisplayName).UserId(newUser.UserId));
				UserSession.Instance.setAuthToken(idToken);
				UserSession.Instance.setUserInfo(userInfo);
				// TODO: DI for GameManager 
				UserSession.Instance.setGameManager(new GameManager());
				SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
			});
		});
	}

	void verifyTokenId(String token) {
		//set sample data
		try {

			var builder = new FlatBufferBuilder(1);
			var tokenId = builder.CreateString(token);
			CredentialToken.StartCredentialToken(builder);
			CredentialToken.AddToken(builder, tokenId);
			var cred = CredentialToken.EndCredentialToken(builder);
			Debug.LogFormat("end message");
			Message.StartMessage(builder);
			Message.AddDataType(builder, schema.Data.CredentialToken);
			Message.AddData(builder, cred.Value);
			var data = Message.EndMessage(builder);
			Debug.LogFormat("Finish");
			builder.Finish(data.Value);
			Debug.LogFormat("get sizebytearray");
			byte[] credBytes = builder.SizedByteArray();

			// TODO: Refactor this into different class
			Debug.Log("Starting to connecting to Netty server...");
			string host = "127.0.0.1";
			int port = 8080;
			socket = new TcpClient(host, port);
			Debug.Log("Setup the socket herer...");
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;

			String str = "";
			for(int i = 0; i < credBytes.Length; ++i) {
				str += (credBytes[i]).ToString() + " ";		
			}
			Debug.Log("Setup the socket byte size:" + credBytes.Length + " token is=" + token);
			Debug.Log("With Value = " + str);

			byte[] bytes = BitConverter.GetBytes(credBytes.Length);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			stream.Write(bytes, 0, bytes.Length);
			stream.Write(credBytes, 0, credBytes.Length);
			stream.Flush();


			int length1 = reader.Read();
			int length2 = reader.Read();
			int length = (length1 << 8) + length2;
			Debug.Log("Data read = " + length1 + ", " + length2 + " .. " + length);


		} catch(Exception e) {
			Debug.Log("Excepion throw: "  + e.ToString());
		}
	}
	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		emailStr = email.GetComponent<InputField> ().text;
		passwordStr = password.GetComponent<InputField> ().text;
	}
}