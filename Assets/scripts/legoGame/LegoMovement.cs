using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;

public class LegoMovement : MonoBehaviour {

	public Rigidbody rb;
	public float speed;
	// private bool pressA;
	// private bool pressD;
	public Text scoreText;
	private float initialPlayerPosition;
	private float newPlayerPosition;
	public static int score;
	
	public GameObject life1;
	public GameObject life2;
	public GameObject life3;
	private List<GameObject> lives;
	private bool endGame;

	private GameObject gameManager;
	// Use this for initialization
	void Start () {
		speed = 500f;	
		initialPlayerPosition = transform.position.z; 
		score = 0;
		lives = new List<GameObject>();
	    lives.Add(life1);
	    lives.Add(life2);
	    lives.Add(life3);
	    endGame = false;
	    gameManager = GameObject.FindWithTag("LegoGameManager");
	    if (HitPlayButton.playRehabilitationLegoGame && !HitPlayButton.playEntertainmentLegoGame) {
 			gameManager.GetComponent<EntertainmentLegoGame>().enabled = false;
 			gameManager.GetComponent<PalmDirectionDetector>().enabled = true;
 			gameManager.GetComponent<PinchDetector>().enabled = true;
	    } else if (!HitPlayButton.playRehabilitationLegoGame && HitPlayButton.playEntertainmentLegoGame) {
 			gameManager.GetComponent<EntertainmentLegoGame>().enabled = true;
 			gameManager.GetComponent<PalmDirectionDetector>().enabled = false;
 			gameManager.GetComponent<PinchDetector>().enabled = false;
	    }
	}
	
	void Update () {
		newPlayerPosition = transform.position.z;
		if (newPlayerPosition - initialPlayerPosition > 15.0f) {
			score += 10;
			scoreText.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
			initialPlayerPosition = newPlayerPosition;
		}
	}


	// Update is called once per frame
	//for calculating the physics functions
	void FixedUpdate () {
		//function arguments; wants to know how much force it should be 
		//added in each direction
		//amount of seconds since the computer drew the last frame
		rb.AddForce(0, 0, speed * Time.fixedDeltaTime);
		//cubul se va roti din cauza frecarii dintre el si ground => 
		// creeam physics material (pe ground)
		if (rb.position.y < -1f && !endGame) {
			endGame = true;
			FindObjectOfType<GameManagement>().EndGame();
		} 
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "Obstacle") {
			if (lives.Count > 1) {
				collisionInfo.gameObject.SetActive (false);
				lives[lives.Count - 1].SetActive(false);
				lives.RemoveAt(lives.Count - 1);
			} else {
				speed = 0;
				lives[lives.Count - 1].SetActive(false);
				//cu scripturi separate pt collision si pt movement => referinta la movementScript -> referinta.enabled = false;
				FindObjectOfType<GameManagement>().EndGame();
			}
		}

		if (collisionInfo.gameObject.tag.Equals("Ground")) {
			EntertainmentLegoGame.playGroundTouched = true;
			PinchDetector.playGroundTouched = true;
		}

	}

	void OnTriggerEnter (Collider colliderInfo) {
		if (colliderInfo.GetComponent<Collider>().tag == "Coin") {
			score += 5;
			scoreText.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
			colliderInfo.gameObject.SetActive (false);
		}
	}

}
