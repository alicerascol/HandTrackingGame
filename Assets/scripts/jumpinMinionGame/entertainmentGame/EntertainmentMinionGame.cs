using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class EntertainmentMinionGame : MonoBehaviour {

    public HandModelBase HandModel;
    private GameObject player;
    private Rigidbody rbPlayer;
    public Hand hand;
    public static bool playGroundTouched;
    public LeapHandController controller;

    private float height;
    private float leftRightDistance;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("PlayerMinionJumping");
        rbPlayer = player.GetComponent<Rigidbody>();
        playGroundTouched = true;
    }
	
    public float speed = 3f;
    void FixedUpdate () {
        hand = HandModel.GetLeapHand();
      	if(HandModel != null && HandModel.IsTracked) {
            //hand.PalmPosition.ToVector3() = position of the camera; hand is a child of the camera
            height = (hand.PalmPosition.y - controller.transform.position.y) * 100;
      		// Debug.Log("height = " + height);
      		leftRightDistance = (hand.PalmPosition.x - controller.transform.position.x) * 100;
      		// Debug.Log("leftRightDistance = " + leftRightDistance);

    		// if(playGroundTouched && height > 20f && leftRightDistance < 0) {
      //           // player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180, player.transform.eulerAngles.z);
      //           rbPlayer.velocity = new Vector3(0f, 7f, 0f);
      //           playGroundTouched = false;
    		// } 
            if(playGroundTouched && height > 20f ) {
                // player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180, player.transform.eulerAngles.z);
                rbPlayer.velocity = new Vector3(0f, 8f, 0f);
                // rbPlayer.AddForce(4f, 8f, 0f);
                playGroundTouched = false;
            } 
            if(leftRightDistance < 0) {
    	    	player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, -90, player.transform.eulerAngles.z);
                player.transform.position += Vector3.left * speed * Time.fixedDeltaTime;
    	  	} 
            if(leftRightDistance > 0) {
    	   		player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90, player.transform.eulerAngles.z);
                player.transform.position += Vector3.right * speed * Time.fixedDeltaTime;
    	  	}
    	}
	}

}