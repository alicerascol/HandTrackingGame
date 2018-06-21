using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAwards : MonoBehaviour {
	public float spinSpeed = 25f;
	// Update is called once per frame
	void Update () {
    	transform.Rotate(spinSpeed * Time.deltaTime, 0,  0);
	}
}
