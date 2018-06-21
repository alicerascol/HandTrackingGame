using UnityEngine;

public class MinionGameWonScript : MonoBehaviour {

	void OnTriggerEnter (Collider colliderInfo) {
		if (colliderInfo.GetComponent<Collider>().tag == "PlayerMinionJumping") {
			FindObjectOfType<GameManagement>().GameWonPanel();
		}
	}
}