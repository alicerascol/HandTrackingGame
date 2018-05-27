using UnityEngine;

public class JumpScript : MonoBehaviour {

	public bool jumpAgain;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		jumpAgain = true;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (jumpAgain && Input.GetKey("a")) { 
			rb.velocity = new Vector3(1.5f, 5f, 0f);
			jumpAgain = false;
		} else	if (jumpAgain && Input.GetKey("d")) { 
			rb.velocity = new Vector3(-1.5f, 5f, 0f);
			jumpAgain = false;
		}
	}

	void OnCollisionEnter (Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Ground") || collisionInfo.gameObject.CompareTag("Brick")) {
			jumpAgain = true;
		}
	}
}
