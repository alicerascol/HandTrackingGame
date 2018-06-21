using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class EntertainmentLegoGame : MonoBehaviour {

	public HandModelBase HandModel;
	public GameObject player;
    Rigidbody rbPlayer;
    public Hand hand;
    public static bool playGroundTouched;
    public LeapHandController controller;

    private float height;
	private float leftRightDistance;

	// Use this for initialization
	public void Start () {
      player = GameObject.FindWithTag("PlayerLegoMovement");
      rbPlayer = player.GetComponent<Rigidbody>();
      playGroundTouched = true;
    }
	
    public float speed = 400f;
    public void FixedUpdate () {
		hand = HandModel.GetLeapHand();
    	if (HandModel != null && HandModel.IsTracked) {
            //hand.PalmPosition.ToVector3() = position of the camera; hand is a child of the camera
        	height = (hand.PalmPosition.y - controller.transform.position.y) * 100;
    		Debug.Log("height = " + height);
    		leftRightDistance = (hand.PalmPosition.x - controller.transform.position.x) * 100;
    		Debug.Log("leftRightDistance = " + leftRightDistance);

			if (playGroundTouched && height > 20f) {
				Debug.Log("jump height = " + height);
		        rbPlayer.velocity = new Vector3(0f, 4f, 0f);
		        rbPlayer.AddForce(0, 0, speed * Time.fixedDeltaTime);
		        playGroundTouched = false;
		    }

		  	if (leftRightDistance < 0 && playGroundTouched) {
		    	rbPlayer.AddForce(-5 * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);
		  	} else if(leftRightDistance > 0 && playGroundTouched) {
		   		rbPlayer.AddForce(5 * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);
		  	}
 	   }
	}

}