using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour {
	public GameObject email;
	public GameObject password;

	private string emailStr;
	private string passwordStr;

	public void RegsiterClick() {
		print ("We are going to register for user :" + emailStr);
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
