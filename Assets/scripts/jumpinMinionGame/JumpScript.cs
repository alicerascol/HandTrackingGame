using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Leap.Unity;

public class JumpScript : MonoBehaviour {

	public static bool jumpAgain;
	public float speed = 10f;
	public static int score;
	public Text scoreText;

	public GameObject life1;
	public GameObject life2;
	public GameObject life3;
	private List<GameObject> lives;
	private GameObject gameManager;

	// Use this for initialization
	void Start () {
		jumpAgain = true;
		score = 0;
		lives = new List<GameObject>();
	    lives.Add(life3);
	    lives.Add(life2);
	    lives.Add(life1);
	    gameManager = GameObject.FindWithTag("MinionGameManager");
	    if (HitPlayButton.playRehabilitationMinionGame && !HitPlayButton.playEntertainmentMinionGame) {
 			gameManager.GetComponent<EntertainmentMinionGame>().enabled = false;
 			gameManager.GetComponent<PalmDirectionDetector>().enabled = true;
 			gameManager.GetComponent<PinchDetector>().enabled = true;
	    } else if (!HitPlayButton.playRehabilitationMinionGame && HitPlayButton.playEntertainmentMinionGame) {
 			gameManager.GetComponent<EntertainmentMinionGame>().enabled = true;
 			gameManager.GetComponent<PalmDirectionDetector>().enabled = false;
 			gameManager.GetComponent<PinchDetector>().enabled = false;
	    }
	}
	
	void OnCollisionEnter (Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Ground")) {
			if (score == 0)	{
				jumpAgain = true;
				EntertainmentMinionGame.playGroundTouched = true;
			} else FindObjectOfType<GameManagement>().EndGame();
		} else if(collisionInfo.gameObject.CompareTag("Brick")) {
			jumpAgain = true;
			EntertainmentMinionGame.playGroundTouched = true;
		} else if (collisionInfo.gameObject.CompareTag ("award")) {
			collisionInfo.gameObject.SetActive (false);
			score += 10;
			scoreText.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
		} else if (collisionInfo.gameObject.CompareTag("MinionObstacle")) {
			if (lives.Count > 1) {
				collisionInfo.gameObject.SetActive (false);
				lives[lives.Count - 1].SetActive(false);
				lives.RemoveAt(lives.Count - 1);
			} else {
				lives[lives.Count - 1].SetActive(false);
				FindObjectOfType<GameManagement>().EndGame();
			}
		}
	}

	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Brick")) {
			jumpAgain = true;
			EntertainmentMinionGame.playGroundTouched = true;
		}
	}

	// void OnTriggerEnter (Collider colliderInfo) {
	// 	if (colliderInfo.GetComponent<Collider>().tag == "Brick") {
	// 		jumpAgain = true;
	// 		Debug.Log("collision detect with brick OnCollisionEnter");
	// 		EntertainmentMinionGame.playGroundTouched = true;
	// 		colliderInfo.gameObject.SetActive (false);
	// 	}
	// }
}
