using UnityEngine;

public class GameWonScript : MonoBehaviour {

	void OnTriggerEnter (Collider colliderInfo) {
		if (colliderInfo.GetComponent<Collider>().tag == "PlayerLegoMovement") {
			Debug.Log("Game Won");
			FindObjectOfType<GameManagement>().GameWonPanel();
		}
	}
}