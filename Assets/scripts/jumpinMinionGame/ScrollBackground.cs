using System.Collections;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

	//higher parralax, slowly the moving
	public float parallax = 2f;
	// Update is called once per frame
	void Update () {
		//the material belongs to the mash renderer
		//for accesing this:
		MeshRenderer mr = GetComponent<MeshRenderer>();
		//material = my specific copy
		Material mat = mr.material;
		Vector2 offset = mat.mainTextureOffset;
		//offset.y = transform.position.y / transform.localScale.y / parralax;
		//offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y += Time.deltaTime / 10f;
		mat.mainTextureOffset = offset;
	}
}
