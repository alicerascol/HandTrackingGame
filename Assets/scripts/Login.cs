using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Login : MonoBehaviour {

	public GameObject username;
	public GameObject password;
	public GameObject loginButton;
	private string Username;
	private string Password;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//tab functionality
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (username.GetComponent<InputField> ().isFocused)
				password.GetComponent<InputField> ().Select();
			if (password.GetComponent<InputField> ().isFocused)
				loginButton.GetComponent<Button> ().Select();
		}
		//in unity variabilele publice trebuie asignate
		Username = username.GetComponent<InputField> ().text;
		Password = password.GetComponent<InputField> ().text;
	}
}
