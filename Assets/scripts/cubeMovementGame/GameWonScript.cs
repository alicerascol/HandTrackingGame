using UnityEngine;

public class GameWonScript : MonoBehaviour {

	void OnTriggerEnter (Collider colliderInfo) {
		if (colliderInfo.GetComponent<Collider>().tag == "PlayerCubeMovement") {
			FindObjectOfType<GameManagement>().GameWonPanel();
		}
	}
}