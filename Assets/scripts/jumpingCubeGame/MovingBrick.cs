using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBrick : MonoBehaviour {

	public float speed = 3f;
	public float marginLeft = 63f;
	public float marginRight = 75f;
	private bool reachedMarginRight = false;
	private bool reachedMarginLeft = true;
	static bool goLeft = false;
	static bool goRight = false;

	// Update is called once per frame
	void Update () {
		if (transform.position.x >= marginRight && !reachedMarginRight) {
			reachedMarginRight = true;
			reachedMarginLeft = false;
		} else
		if (transform.position.x < marginRight && !reachedMarginRight) {
			transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
			goRight = true;
			goLeft = false;
		} else
		if (transform.position.x <= marginLeft && !reachedMarginLeft) {
			reachedMarginRight = false;
			reachedMarginLeft = true;
		} else
		if (transform.position.x > marginLeft && !reachedMarginLeft) {
			transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
			goRight = false;
			goLeft = true;
		}
	}

	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("PlayerJumpingCube")) {
			if(goLeft && !goRight) {
				collisionInfo.gameObject.transform.position = new Vector3(collisionInfo.gameObject.transform.position.x + speed * Time.deltaTime, collisionInfo.gameObject.transform.position.y, collisionInfo.gameObject.transform.position.z);
			} else if(!goLeft && goRight) {
				collisionInfo.gameObject.transform.position = new Vector3(collisionInfo.gameObject.transform.position.x - speed * Time.deltaTime, collisionInfo.gameObject.transform.position.y, collisionInfo.gameObject.transform.position.z);
			}
		}
	}
}
