using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void  OnCollisionEnter(Collision colliderInfo) {
		Debug.Log ("collision detected with " + colliderInfo.gameObject.GetComponent<Collider> ().name);
	}

	void OnCollisionStay (Collision colliderInfo) {
		// transform.position = colliderInfo.gameObject.GetComponent<Collider> ().transform.position;
	}
}
