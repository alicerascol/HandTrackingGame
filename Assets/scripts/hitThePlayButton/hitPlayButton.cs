using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;

public class hitPlayButton : MonoBehaviour {

	public Text welcomeText;
	public void Start () {
		welcomeText.text += (Login.Username == null || Login.Username.ToString().Equals("")) ?  "!" : ", " + (Login.Username.ToString() + "!");
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
}
