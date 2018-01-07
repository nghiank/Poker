using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

	public GameObject email;
	public GameObject password;

	private string emailStr;
	private string passwordStr;


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
		});
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		emailStr = email.GetComponent<InputField> ().text;
		passwordStr = password.GetComponent<InputField> ().text;
	}
}
