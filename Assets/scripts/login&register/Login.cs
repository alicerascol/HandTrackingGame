using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public GameObject username;
	public GameObject password;
	public Button loginButton;
	public static string Username;
	private string Password;

	public Text retryText;
	public GameObject retryPanel;
	private float restartDelay = 4f;

	private string CheckIfExistsURL = "http://localhost/unity_game/check_for_login.php";

	// public Button button;
	private bool buttonPressed = false;
	public static WWW wwwSession;

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

	public void LoginButton() {
		if(Username == "" || Password == "" ||	Username == null || Password == null) {
			retryPanel.SetActive(true);
        	retryText.text = "You must complete all the fields!";
        	StartCoroutine(WaitBeforeNextStep());
        	retryPanel.SetActive(false);
        } else {
			WWWForm form = new WWWForm();
			//making the form
			form.AddField("usernamePost", Username);
			form.AddField("passwordPost", Password);
			Debug.Log(Username + " " + Password + " " + form);
			WWW itemsData = new WWW(CheckIfExistsURL, form);
			CheckIfExists(itemsData, Username, Password);
		}
	}

	public static string userId;
	//Our Coroutine for getting the data
	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		// check for errors
	    if (www.error == null) {
		    userId = www.text.ToString().Remove(www.text.ToString().Length - 1).Substring(www.text.ToString().Length - 5);
	    	if (www.text.ToString().StartsWith("User has logged in")) {
	        	//treci la scena urmatoare
	        	Invoke("LoadNextScene", restartDelay);
	        } else {
	        	retryPanel.SetActive(true);
	        	retryText.text = www.text.ToString();
	        	Invoke("RestartScene", restartDelay);
	        }
	    } else {
	        Debug.Log("WWW Error: "+ www.error);
	    } 
	}

 	//verifica daca exista userul in baza de date
 	//daca exista, pune id pe sesiune
	public void CheckIfExists(WWW itemsData, string username, string password) {
		//Start the Coroutine
        StartCoroutine(WaitForRequest(itemsData));
	}

	public void RestartScene() {
		//current scene -> 
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name)
		SceneManager.LoadScene("Login");
	}
	
	public void LoadNextScene() {
		SceneManager.LoadScene("HitPlayButton");
	}

	public void backToTheFirstScene () {
		SceneManager.LoadScene("FirstScene");
	}
	
	IEnumerator WaitBeforeNextStep() {
	    yield return new WaitForSeconds(3f);
	}
}