using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour {

	public GameObject username;
	public GameObject email;
	public GameObject password;
	public GameObject confirmPassword;
	public Button registerButton;
	private string Username;
	private string Email;
	private string Password;
	private string ConfirmPassword;

	private string CreateUserURL = "http://localhost/unity_game/insert_new_user.php";
	private string CheckIfExistsURL = "http://localhost/unity_game/check_if_user_exists.php";
	private string startSessionURL = "http://localhost/unity_game/start_session.php";
	private string getUserIdURL = "http://localhost/unity_game/get_user_id.php";
	
	private bool userIsCreated = false;

	public Text retryText;
	public GameObject retryPanel;
	private float restartDelay = 2f;

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
		
		if(Username != "" && Password != "" && Email != "" && ConfirmPassword != "" && !userIsCreated) {
			userIsCreated = true;

	        WWWForm form = new WWWForm();
			//making the form
			form.AddField("usernamePost", Username);
			form.AddField("emailPost", Email);
			WWW itemsData = new WWW(CheckIfExistsURL, form);
			registerButton.onClick.AddListener(delegate {CheckIfExists(itemsData, Username, Password, Email);});

		}
	}

	private string response;
    //Our Coroutine for getting the data
	IEnumerator WaitForRequest(WWW www) {
	    yield return www;
	     
	    //  // check for errors
	    // if (www.error == null) {
	    //     //Assign the data that was fetched to the variable answer
		   //  response = www.text.ToString();
	    //     Debug.Log(response);
	    // } else {
	    //     Debug.Log("WWW Error: "+ www.error);
	    // }    
 	}
 

	public void CheckIfExists(WWW itemsData, string username, string password, string email) {
		//Start the Coroutine
        StartCoroutine(WaitForRequest(itemsData));
        if (itemsData.text.ToString().Equals("Everything ok.")) {
        	CreateUser(username, password, email);

   //      	string userId = itemsData.text.ToString().Substring(itemsData.text.ToString().Length - 5);
			// Debug.Log("userId = " + userId);
			// WWWForm form = new WWWForm();
			// form.AddField("idPost", userId);
   //      	WWW www = new WWW(startSessionURL, form);
   //      	if (www.error != null) {
		 //        Debug.Log("WWW Error: " + www.error);
		 //    } else Debug.Log("Session started!");

        	//treci la scena urmatoare
        } else {
        	retryPanel.SetActive(true);
        	retryText.text = itemsData.text.ToString();
        	Invoke("RestartScene", restartDelay);

        }
	}

	public void RestartScene() {
		//current scene -> 
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name)
		SceneManager.LoadScene("RegisterAndLogin");
	}

	public void CreateUser(string username, string password, string email){
		//class that allows us to send a form to a php; trigger our php from unuity
		WWWForm form = new WWWForm();
		//making the form
		form.AddField("usernamePost", username);
		form.AddField("passwordPost", password);
		form.AddField("emailPost", email);
		//simple access to web pages
		//connect with the url and send the form to the php file
		WWW www = new WWW(CreateUserURL, form);
		Debug.Log("Registration completed");
	}
}
