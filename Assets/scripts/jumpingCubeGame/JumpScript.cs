using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class JumpScript : MonoBehaviour {

	public static bool jumpAgain;
	private Rigidbody rb;
	public float speed = 10f;
	private int score;
	public Text scoreText;

	public GameObject life1;
	public GameObject life2;
	public GameObject life3;
	private List<GameObject> lives;

	// Use this for initialization
	void Start () {
		jumpAgain = true;
		rb = GetComponent<Rigidbody>();
		score = 0;
		lives = new List<GameObject>();
	    lives.Add(life3);
	    lives.Add(life2);
	    lives.Add(life1);
	}
	
	private bool rightPosition = false;
	private bool leftPosition = false;
	private bool normalPosition = true;

	// Update is called once per frame
	void Update () {
		if (jumpAgain && Input.GetKey("a")) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
			rb.velocity = new Vector3(2f, 7f, 0f);
			jumpAgain = false;
		} else if (jumpAgain && Input.GetKey("d")) { 
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
			rb.velocity = new Vector3(-2f, 7f, 0f);
			jumpAgain = false;
		} else if (Input.GetKey("q")) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (Input.GetKey("w")) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	void OnCollisionEnter (Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Ground")) {
			if (score == 0)	jumpAgain = true;
			else FindObjectOfType<JumpGameManagement>().EndGame();
		} else if(collisionInfo.gameObject.CompareTag("Brick")) {
			jumpAgain = true;
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
				FindObjectOfType<JumpGameManagement>().EndGame();
			}
		}
	}

	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Brick")) {
			jumpAgain = true;
		}
	}
}
