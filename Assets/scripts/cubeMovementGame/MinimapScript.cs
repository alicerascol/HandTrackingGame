using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour {

	public Transform player;

	//called after update and fixedupdate -> vrem sa fixam pozitia minimapului dupa ce 
	//e fixata cea a playerului
	void LateUpdate() {
		Vector3 newPositionMinimap = player.position;
		newPositionMinimap.y = transform.position.y;
		transform.position = newPositionMinimap;
	}
}
