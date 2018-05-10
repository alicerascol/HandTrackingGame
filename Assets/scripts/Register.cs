using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Register : MonoBehaviour {

	public GameObject username;
	public GameObject email;
	public GameObject password;
	public GameObject confirmPassword;
	public GameObject registerButton;
	private string Username;
	private string Email;
	private string Password;
	private string ConfirmPassword;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		//tab functionality
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (username.GetComponent<InputField> ().isFocused)
				email.GetComponent<InputField> ().Select();
			if (email.GetComponent<InputField> ().isFocused)
				password.GetComponent<InputField> ().Select();
			if (password.GetComponent<InputField> ().isFocused)
				confirmPassword.GetComponent<InputField> ().Select();
			if (confirmPassword.GetComponent<InputField> ().isFocused)
				registerButton.GetComponent<Button> ().Select();
		}
		//in unity variabilele publice trebuie asignate
		Username = username.GetComponent<InputField> ().text;
		Email = email.GetComponent<InputField> ().text;
		Password = password.GetComponent<InputField> ().text;
		ConfirmPassword = confirmPassword.GetComponent<InputField> ().text;

	}
}
