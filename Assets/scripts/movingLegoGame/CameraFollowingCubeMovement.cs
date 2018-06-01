using UnityEngine;

public class CameraFollowingCubeMovement : MonoBehaviour {

	//referinta catre player/camera 
	public Transform player;
	public Vector3 offset = new Vector3(0, 2, -6);
	// Update is called once per frame
	void Update () {
		//transfrm component of the current object/ the object the script sits
		transform.position = player.position + offset;
	}

}
