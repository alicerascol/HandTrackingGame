using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	//
	// VARIABLES
	//
	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 4.0f;		// Speed of the camera when being panned
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;		// Is the camera being panned?
	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?

	public float speed = 0.1f;
	public float distance = 1f;
	private bool collisionSouth = false;
	private bool collisionNorth = false;
	private bool collisionWest = false;
	private bool collisionEst = false;

/*	private Rigidbody rigidbody;
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}
		
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidbody.AddForce (movement * 5);
	}
*/
	public void FixedUpdate() {
		if(Input.GetKey(KeyCode.RightArrow))
		{

			if (collisionNorth) {
				collisionNorth = false;
				transform.position = new Vector3 (transform.position.x - distance, transform.position.y, transform.position.z);
				speed = 0.1f;
			} else {
				transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z);
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {	
			if (collisionSouth) {
				collisionSouth = false;
				speed = 0.1f;
				transform.position = new Vector3 (transform.position.x + distance, transform.position.y, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);
			}
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			if (collisionWest) {
				collisionWest = false;
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + distance);
				speed = 0.1f;
			} else {
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed);
			}
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			if (collisionEst) {
				collisionEst = false;
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - distance);
				speed = 0.1f;
			} else {
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed);
			}
		}
			
	}
		
	void Update () {		

		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
			

		// Get the middle mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isZooming = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton(0)) isRotating=false;
		if (!Input.GetMouseButton(1)) isZooming=false;

		// Rotate camera along X and Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);
			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}
			

		// Move the camera linearly along Z axis
		if (isZooming)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			Vector3 move = pos.y * zoomSpeed * transform.forward; 
			transform.Translate(move, Space.World);
		}
	}

	void  OnCollisionEnter(Collision colliderInfo) {
		if (colliderInfo.gameObject.CompareTag("NorthWall") ) {
			Debug.Log ("collision detected with " + colliderInfo.gameObject.GetComponent<Collider> ().name);
			if (Input.GetKey (KeyCode.LeftArrow))
				speed = 0;
			collisionNorth = true;
		} else if (colliderInfo.gameObject.CompareTag("SouthWall") ) {
			Debug.Log ("collision detected with " + colliderInfo.gameObject.GetComponent<Collider> ().name);
			if (Input.GetKey (KeyCode.RightArrow)) 
				speed = 0;
			collisionSouth = true;
		} else if (colliderInfo.gameObject.CompareTag("EstWall") ) {
			Debug.Log ("collision detected with " + colliderInfo.gameObject.GetComponent<Collider> ().name);
			if (Input.GetKey (KeyCode.DownArrow))
				speed = 0;
			collisionEst = true;
		} else if (colliderInfo.gameObject.CompareTag("WestWall") ) {
			Debug.Log ("collision detected with " + colliderInfo.gameObject.GetComponent<Collider> ().name);
			if (Input.GetKey (KeyCode.UpArrow)) 
				speed = 0;
			collisionWest = true;
		}
	}

	/*
	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag ("NorthWall") ||
			collisionInfo.gameObject.CompareTag ("WestWall") ||
			collisionInfo.gameObject.CompareTag ("EstWall") ||
			collisionInfo.gameObject.CompareTag ("SouthWall")) {

			//Stop rotating
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.freezeRotation = true;
			Debug.Log ("OnCollisionStay detected with" + collisionInfo.gameObject.GetComponent<Collider> ().name);
		}
	}

	void OnCollisionEnter(Collision collisionInfo) {

		Rigidbody rbdy = collisionInfo.gameObject.GetComponent<Rigidbody>();
		if (collisionInfo.gameObject.CompareTag("NorthWall") || 
			collisionInfo.gameObject.CompareTag("WestWall") ||
			collisionInfo.gameObject.CompareTag("EstWall") ||
			collisionInfo.gameObject.CompareTag("SouthWall")) {
			//Stop Moving/Translating
			rigidbody.velocity = Vector3.zero;
			//Stop rotating
			rigidbody.angularVelocity = Vector3.zero;
			Debug.Log ("OnCollisionEnter detected with" + collisionInfo.gameObject.GetComponent<Collider> ().name);
		}
	}

	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag ("NorthWall") ||
		    collisionInfo.gameObject.CompareTag ("WestWall") ||
		    collisionInfo.gameObject.CompareTag ("EstWall") ||
		    collisionInfo.gameObject.CompareTag ("SouthWall")) {

			//Stop rotating
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.freezeRotation = true;
			Debug.Log ("OnCollisionStay detected with" + collisionInfo.gameObject.GetComponent<Collider> ().name);
		}
	}
*/


}

