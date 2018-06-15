using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;
using UnityEngine.UI;

public class HitPlayButton : MonoBehaviour {

	private string getScoresURL = "http://localhost/unity_game/get_scores_for_user.php";
	public static string jumpingMinionScore;
	public static string movingLegoScore;
	public static string lastGameScore;

	public Text welcomeText;
	public void Start () {
		string text = "!";
		if (!(Login.Username == null || Login.Username.ToString().Equals(""))) {
			GetScoresForUser();
			text = ", " + Login.Username.ToString() + "!";
		}
		if (!(Register.Username == null || Register.Username.ToString().Equals(""))) {
			GetScoresForUser(); 
			text = ", " + Register.Username.ToString() + "!";
		}
		welcomeText.text += text;
	}

	public void playLLGame () {
		SceneManager.LoadScene("MovingLegoGame");
	}

	public void playJMGame () {
		SceneManager.LoadScene("JumpingMinionGame");
	}

	public void QuitGame() {
		Debug.Log("Quit!");
		Application.Quit();
	}

	IEnumerator WaitForRequestGetScores(WWW www) {
	    yield return www;
	     
	    // check for errors
	    if (string.IsNullOrEmpty(www.error)) {
	        //Assign the data that was fetched to the variable answer
	        Debug.Log(www.text);
	        if(!www.text.StartsWith("Could not get scores for the user")) {
		        String[] substrings = www.text.ToString().Remove(www.text.ToString().Length - 1).Split('|');
		        lastGameScore = substrings[2].Substring(14);
		        Debug.Log(substrings[2] + " -> lastGameScore = " + lastGameScore);
		        movingLegoScore = substrings[1].Substring(16);
		        Debug.Log(substrings[1] + "-> movingLegoScore = " + movingLegoScore);
		        jumpingMinionScore = substrings[0].Substring(19);
		        Debug.Log(substrings[0] + "-> jumpingMinionScore = " + jumpingMinionScore);
		    }
	    } else {
	        Debug.Log(www.error);
	    }
 	}

	public void GetScoresForUser() {
		WWWForm form = new WWWForm();
		//making the form
		form.AddField("idPost", Register.userId == null ? Login.userId : Register.userId);
		WWW www = new WWW(getScoresURL, form);
		StartCoroutine(WaitForRequestGetScores(www));
	}

}
