using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public GameObject username;
	public GameObject password;
	public Button loginButton;
	private string Username;
	private string Password;

	public Text retryText;
	public GameObject retryPanel;
	private float restartDelay = 2f;
	private bool hasLoggedIn = false;

	private string CheckIfExistsURL = "http://localhost/unity_game/check_for_login.php";


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
 

	public void CheckIfExists(WWW itemsData, string username, string password) {
		//Start the Coroutine
        StartCoroutine(WaitForRequest(itemsData));
        if (itemsData.text.ToString().Equals("User has logged in")) {
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
}