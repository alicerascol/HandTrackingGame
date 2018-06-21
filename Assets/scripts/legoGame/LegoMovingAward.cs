using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoMovingAward : MonoBehaviour {

	private float spinSpeed = 25f;
	void Update () {
    	transform.Rotate(0, spinSpeed * Time.deltaTime,  0);
	}
}
