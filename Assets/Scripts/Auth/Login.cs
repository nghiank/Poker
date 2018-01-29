using System.Collections;
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
	private StreamReader reader;

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
				UserSession userSession = new UserSession();
				UserInfo userInfo = new UserInfo(new UserInfo.UserInfoBuilder().DisplayName(newUser.DisplayName).UserId(newUser.UserId));
				userSession.SetUserInfo(userInfo);
				userSession.SetAuthToken(idToken);
				// TODO: DI for GameManager 
				userSession.SetGameManager(new GameManager());
				ClientContext.Instance.SetUserSession(userSession);
				SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
			});
		});
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
