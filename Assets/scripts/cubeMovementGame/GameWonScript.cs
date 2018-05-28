using UnityEngine;

public class GameWonScript : MonoBehaviour {

	void OnTriggerEnter (Collider colliderInfo) {
		if (colliderInfo.GetComponent<Collider>().tag == "PlayerLegoMovement") {
			FindObjectOfType<GameManagement>().GameWonPanel();
		}
	}
}