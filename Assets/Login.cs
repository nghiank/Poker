using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using FlatBuffers;
using Schema;
using System;

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

	public class NettyData
	{
		public int eventType;
		public string username;
	}

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


			Debug.LogFormat("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
			Debug.LogFormat("User data:" + newUser.UserId);

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
				verifyTokenId(idToken);
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
			Message.AddDataType(builder, Schema.Data.CredentialToken);
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
			Debug.Log("Setup the socket byte size:" + credBytes.Length);
			Debug.Log("With Value = " + str);

			stream.Write(credBytes, 0, credBytes.Length);
			stream.Flush();
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
