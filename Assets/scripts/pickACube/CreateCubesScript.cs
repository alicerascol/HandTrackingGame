using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class CreateCubesScript : MonoBehaviour {

	List<GameObject> cubes;
	List<float> xz = new List<float>{
		-0.1f,
		-0.2f,
		-0.3f,
		-0.4f,
		-0.5f,
		-0.6f,
		-0.7f,
		-0.8f,
		-0.9f,
		1f,
		0.1f,
		0.2f,
		0.3f,
		0.4f,
		0.5f,
		0.6f,
		0.7f,
		0.8f
	};
	private IEnumerator coroutine;
	// GameObject go;


	void Start() {
		// Camera cam = GameObject.Find("newCamera").GetComponent<Camera>();
		// Debug.Log("am gasit Camera");
		cubes = new List<GameObject> ();
		// coroutine = WaitAndAddCube (10f);
		// StartCoroutine (coroutine);
	}

	GameObject createACube () {
		Debug.Log ("am adaugat un cub");
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.transform.position = chooseRandomPosition ();
		Rigidbody cubeRigidBody = cube.AddComponent<Rigidbody>();
		cubeRigidBody.useGravity = true;
		BoxCollider boxcollider = cube.AddComponent<BoxCollider>();
		return cube;
	}

	Vector3 chooseRandomPosition() {
		float x = xz[Random.Range (0, xz.Count + 1)];
		float y = 15f;
		float z = xz[Random.Range (0, xz.Count + 1)];
		Debug.Log("chooseRandomPosition " + x + ", " +  y + ", " + z);
		return new Vector3 (x, y, z); 
	}

	private IEnumerator WaitAndAddCube (float waitTime) {
		while (true) {
			cubes.Add (createACube ());
			Debug.Log ("Add Cubes in loop");
			yield return new WaitForSeconds (waitTime);
		}
	}
		
}


