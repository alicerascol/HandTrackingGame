using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour {

	public Rigidbody rb;
	public float speed;
	public float leftRightSpeed;
	private bool pressA;
	private bool pressD;
	// Use this for initialization
	void Start () {
		speed = 800f;
		leftRightSpeed = 400f;
	}
	
	void Update () {

		if (Input.GetKey("d")) {
			pressD = true;
			pressA = false;
		}
		if (Input.GetKey("a")) {
			pressA = true;
			pressD = false;
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

		if (pressD) {
			rb.AddForce(500 * Time.fixedDeltaTime, 0, 0);
		}
		if (pressA) {
			rb.AddForce(-500 * Time.fixedDeltaTime, 0, 0);
		}
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "Obstacle") {
			speed = 0;
			leftRightSpeed = 0;
			//cu scripturi separate pt collision si pt movement => referinta la movementScript -> referinta.enabled = false;
		}
	}
}
