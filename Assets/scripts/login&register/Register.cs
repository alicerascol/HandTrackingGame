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
	public static string Username;
	private string Email;
	private string Password;
	private string ConfirmPassword;

	private string CreateUserURL = "http://localhost/unity_game/insert_new_user.php";
	private string CheckIfExistsURL = "http://localhost/unity_game/check_if_user_exists.php";
	private string getUserIdURL = "http://localhost/unity_game/get_user_id.php";
	private string insertScoreForUserURL = "http://localhost/unity_game/insert_scores_for_user.php";

	public Text retryText;
	public GameObject retryPanel;
	private float restartDelay = 4f;
	public static WWW wwwSession;


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

	public void RegisterButton() {
		if(Username == "" || Password == "" || Email == "" || ConfirmPassword == "" ||
			Username == null || Password == null || Email == null || ConfirmPassword == null) {
			retryPanel.SetActive(true);
        	retryText.text = "You must complete all the fields!";
        	StartCoroutine(WaitBeforSetActiveFalse());
        } else {
        	if (!Password.Equals(ConfirmPassword)) {
        		retryPanel.SetActive(true);
	        	retryText.text = "Password and Confirm Password must be the same!";
	        	StartCoroutine(WaitBeforSetActiveFalse());
	        } else {
				WWWForm form = new WWWForm();
				//making the form
				form.AddField("usernamePost", Username);
				form.AddField("emailPost", Email);
				WWW itemsData = new WWW(CheckIfExistsURL, form);
				CheckIfExists(itemsData);
			}
		}
	}
		
	IEnumerator WaitBeforSetActiveFalse() {
	    yield return new WaitForSeconds(3f);
	    retryPanel.SetActive(false);
	}

	private string response;
    //Our Coroutine for getting the data
	IEnumerator WaitForRequest(WWW www) {
	    yield return www;
	    if (www.text.ToString().Equals("Everything ok.")) {
        	CreateUser(Username, Password, Email);
        	//iei user id si pui in variabila statica
        	GetUserId();
   			//treci la scena urmatoare
        	Invoke("LoadNextScene", restartDelay);
        } else {
        	Debug.Log(www.text.ToString());
        	retryPanel.SetActive(true);
        	retryText.text = www.text.ToString();
        	Invoke("RestartScene", restartDelay);
        }
 	}

 	public static string userId;
 	IEnumerator WaitForRequestUserId(WWW www) {
	    yield return www;
	    // check for errors
	    if (www.error == null) {
	        //Assign the data that was fetched to the variable answer
		    userId = www.text.ToString().Remove(www.text.ToString().Length - 1).Substring(www.text.ToString().Length - 5);
		    CreateScoreEntryForUser(); 
	        Debug.Log("userId = " + userId);
	    } else {
	        Debug.Log("WWW Error: " + www.error);
	    }    
 	}

	public void CheckIfExists(WWW itemsData) {
		//Start the Coroutine
        StartCoroutine(WaitForRequest(itemsData));
	}

	public void RestartScene() {
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name)
		SceneManager.LoadScene("Register");
	}

	public void LoadNextScene() {
		SceneManager.LoadScene("HitPlayButton");
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

	public void GetUserId() {
 		WWWForm form = new WWWForm();
		//making the form
		form.AddField("usernamePost", Username);
		WWW www = new WWW(getUserIdURL, form);
		StartCoroutine(WaitForRequestUserId(www));
 	}

	public void backToTheFirstScene () {
		SceneManager.LoadScene("FirstScene");
	}

	public void CreateScoreEntryForUser() {
		Debug.Log("CreateScoreEntryForUser ->" + userId);
		WWWForm form = new WWWForm();
		//making the form
		Debug.Log(userId);
		form.AddField("idPost", Register.userId);
		WWW www = new WWW(insertScoreForUserURL, form);
		StartCoroutine(WaitForRequestInsertScore(www));
	}

	IEnumerator WaitForRequestInsertScore(WWW www) {
	    yield return www;
	    // check for errors
	    if (www.error == null) {
	        //Assign the data that was fetched to the variable answer
	        Debug.Log(www.text);
	    } else {
	        Debug.Log(www.error);
	    }    
 	}

}
