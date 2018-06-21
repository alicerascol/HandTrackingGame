using UnityEngine;
//folosit cand trebe sa schimbam scena sau 
//sa facem reload pe scena in care suntem
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManagement : MonoBehaviour {

	private bool gameHasEnded = false;
	private float restartDelay = 3f;
	public GameObject gameWonPanel;
	public GameObject gamePausePanel;
	public Text gameWonText;
	public GameObject retryPanel;
	public Text retryText;
	private string PostNewBestScoreURL = "http://localhost/unity_game/update_scores.php";

	void Update () {
		if (Input.GetKey("q")) {
			Time.timeScale = 0;
			GamePausePanel();
		}
	}

	public void PostNewBestScore (string gameName, string gameScore) {
		WWWForm form = new WWWForm();
		//making the form
		form.AddField("idPost", Register.userId != null ? Register.userId : Login.userId);
		if (gameName.Equals("MovingLegoGame")) {
			form.AddField("jumpingMinionScorePost", HitPlayButton.jumpingMinionScore);
			form.AddField("movingLegoScorePost", gameScore);
			form.AddField("lastGameScorePost", HitPlayButton.lastGameScore);
		} else if (gameName.Equals("JumpingMinionGame")) {
			form.AddField("jumpingMinionScorePost", gameScore);
			form.AddField("movingLegoScorePost", HitPlayButton.movingLegoScore);
			form.AddField("lastGameScorePost", HitPlayButton.lastGameScore);
		}
		WWW itemsData = new WWW(PostNewBestScoreURL, form);
		StartCoroutine(WaitForRequest(itemsData));
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		Debug.Log(www.text);
	}

	public void ScoreManager () {
		int new_score;
		int last_score;
		if (SceneManager.GetActiveScene().name.Equals("MovingLegoGame")) {
			new_score = LegoMovement.score;
			last_score = Int32.Parse(HitPlayButton.movingLegoScore);
			if (new_score > last_score) {
				HitPlayButton.movingLegoScore = new_score.ToString();
				retryText.text = "New best score: " + new_score.ToString();
				PostNewBestScore("MovingLegoGame", new_score.ToString());
			} else retryText.text = "Your score: " + new_score.ToString();
		} else if (SceneManager.GetActiveScene().name.Equals("JumpingMinionGame")) {
			new_score = JumpScript.score;
			last_score = Int32.Parse(HitPlayButton.jumpingMinionScore);
			if (new_score > last_score) {
				HitPlayButton.jumpingMinionScore = new_score.ToString();
				retryText.text = "New best score: " + new_score.ToString();
				PostNewBestScore("JumpingMinionGame", new_score.ToString());
			} else retryText.text = "Your score: " + new_score.ToString();
		}
	}

	public void EndGame() {
		if (!gameHasEnded) {
			gameHasEnded = true;
			Debug.Log("game over!");
			if (Register.userId != null || Login.userId != null)
				ScoreManager();
			else SetScoreForGuest();
			retryPanel.SetActive(true);
			Invoke("RestartTheSameGame", restartDelay);
		}
	}

	public void SetScoreForGuest() {
		if (SceneManager.GetActiveScene().name.Equals("MovingLegoGame")) {
			retryText.text = "Your score: " + LegoMovement.score;
		} else if (SceneManager.GetActiveScene().name.Equals("JumpingMinionGame")) {
			retryText.text = "Your score: " + JumpScript.score;
		}
	}

	public void RestartTheSameGame() {
		gameHasEnded = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GameWonPanel() {
		Debug.Log("GameWon");
		if (Register.userId != null || Login.userId != null)
			ScoreManager();
		else SetScoreForGuest();
		gameWonPanel.SetActive(true);
		Invoke("RestartGame", restartDelay);
	}

	public void RestartGame() {
		gameHasEnded = false;
		Time.timeScale = 1;
		SceneManager.LoadScene("HitPlayButton");
	}

	public void GamePausePanel() {
		gamePausePanel.SetActive(true);
	}

	public void Play() {
		gamePausePanel.SetActive(false);
		Time.timeScale = 1;
	}
}
