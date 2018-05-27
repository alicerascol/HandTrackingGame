using UnityEngine;
//folosit cand trebe sa schimbam scena sau 
//sa facem reload pe scena in care suntem
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {

	private bool gameHasEnded = false;
	public float restartDelay = 2f;
	public GameObject gameWonPanel;

	public void EndGame() {
		if (!gameHasEnded) {
			gameHasEnded = true;
			Debug.Log("game over!");
			Invoke("RestartGame", restartDelay);
		}
	}

	public void RestartGame() {
		//current scene -> 
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name)
		SceneManager.LoadScene("MovingCubeGame");
	}

	public void GameWonPanel() {
		Debug.Log("GameWon");
		gameWonPanel.SetActive(true);
	}
}
