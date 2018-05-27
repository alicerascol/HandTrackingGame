using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMovement : MonoBehaviour {

	public Rigidbody rb;
	public float speed;
	// private bool pressA;
	// private bool pressD;
	public Text scoreText;
	private float initialPlayerPosition;
	private float newPlayerPosition;
	private int score;
	
	// Use this for initialization
	void Start () {
		speed = 900f;	
		initialPlayerPosition = transform.position.z; 
		score = 0;
	}
	
	void Update () {

		// if (Input.GetKey("d")) {
		// 	pressD = true;
		// 	pressA = false;
		// }
		// if (Input.GetKey("a")) {
		// 	pressA = true;
		// 	pressD = false;
		// }
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
		if (rb.position.y < -1f) {
			FindObjectOfType<GameManagement>().EndGame();
		} 
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "Obstacle") {
			speed = 0;
			//cu scripturi separate pt collision si pt movement => referinta la movementScript -> referinta.enabled = false;
			FindObjectOfType<GameManagement>().EndGame();
		}
	}

}
