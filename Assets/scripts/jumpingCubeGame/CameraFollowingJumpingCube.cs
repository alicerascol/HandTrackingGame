using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowingJumpingCube : MonoBehaviour {

	//referinta catre player
	public Transform player;
	public Vector3 offset = new Vector3(0, 1, -15);
	// Update is called once per frame
	void Update () {
		//transfrm component of the current object/ the object the script sits
		transform.position = player.position + offset;
	}

}
