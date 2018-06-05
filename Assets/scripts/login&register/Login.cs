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
	private float restartDelay = 2f;
	private bool hasLoggedIn = false;

	private string CheckIfExistsURL = "http://localhost/unity_game/check_for_login.php";
	private string startSessionURL = "http://localhost/unity_game/start_session.php";
	private string getUserIdFromSessionURL = "http://localhost/unity_game/get_id_from_session.php";


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
		if(Username != "" && Password.Length == 6 && !hasLoggedIn) {
			hasLoggedIn = true; 
			WWWForm form = new WWWForm();
			//making the form
			form.AddField("usernamePost", Username);
			form.AddField("passwordPost", Password);
			Debug.Log(Username + " " + Password + " " + form);
			WWW itemsData = new WWW(CheckIfExistsURL, form);
			loginButton.onClick.AddListener(delegate {CheckIfExists(itemsData, Username, Password);});
		}

		// button.onClick.AddListener(delegate {getUserIdFromSession(wwwSession);});
	}


    //Our Coroutine for getting the data
	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		// check for errors
	    if (www.error == null) {
	        Debug.Log(www.text);
	    } else {
	        Debug.Log("WWW Error: "+ www.error);
	    } 
	}

	//asteapta raspunsul;
	//stocheaza id-ul sesiunii la cheia "sessionIdPost"
	//salveaza static obiectul de tip WWW pentru acces simplu la pagini web
	IEnumerator WaitForRequestForSession(WWW www) {
		yield return www;
	     
	     // check for errors
	    if (www.error == null) {
	        //Assign the data that was fetched to the variable answer
		    string sessionId = www.text.ToString();
		    WWWForm form = new WWWForm();
			//making the form
			form.AddField("sessionIdPost", sessionId);
			wwwSession = new WWW(getUserIdFromSessionURL, form);
	        Debug.Log("sessionId = " + sessionId);
	    } else {
	        Debug.Log("WWW Error: "+ www.error);
	    }    
 	}
 
 	//verifica daca exista userul in baza de date
 	//daca exista, pune id pe sesiune
	public void CheckIfExists(WWW itemsData, string username, string password) {
		//Start the Coroutine
        StartCoroutine(WaitForRequest(itemsData));
        if (itemsData.text.ToString().StartsWith("User has logged in")) {
			putIdOnTheSession(itemsData.text.ToString().Substring(itemsData.text.ToString().Length - 5));
        	//treci la scena urmatoare
        	Invoke("LoadNextScene", restartDelay);
        } else {
        	Debug.Log("nu exista user");
        	retryPanel.SetActive(true);
        	retryText.text = itemsData.text.ToString();
        	Invoke("RestartScene", restartDelay);
        }
	}
	public void RestartScene() {
		//current scene -> 
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name)
		SceneManager.LoadScene("FirstScene");
	}
	public void LoadNextScene() {
		SceneManager.LoadScene("HitPlayButton");
	}

	public void putIdOnTheSession (string userId) {
		//dupa ce s a logat user ul iei id-ul
		Debug.Log("userId = " + userId);
		WWWForm form = new WWWForm();
		form.AddField("idPost", userId);
		//pui pe sesiune id ul userului
    	WWW www = new WWW(startSessionURL, form);
       	StartCoroutine(WaitForRequestForSession(www));
    }


	public void getUserIdFromSession(WWW www) {
		if (!buttonPressed) {
    	   	StartCoroutine(WaitForRequest(www));
			buttonPressed = true;
		}
	}
}