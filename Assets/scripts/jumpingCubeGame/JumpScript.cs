using UnityEngine;
using UnityEngine.UI;

public class JumpScript : MonoBehaviour {

	public bool jumpAgain;
	private Rigidbody rb;
	public float speed = 10f;
	private int score;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		jumpAgain = true;
		rb = GetComponent<Rigidbody>();
		score = 0;
	}
	
	static bool rightPosition = false;
	static bool leftPosition = false;
	static bool normalPosition = true;

	// Update is called once per frame
	void Update () {
		if (jumpAgain && Input.GetKey("a")) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
			rb.velocity = new Vector3(2f, 7f, 0f);
			jumpAgain = false;
		} else if (jumpAgain && Input.GetKey("d")) { 
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
			rb.velocity = new Vector3(-2f, 7f, 0f);
			jumpAgain = false;
		} else if (Input.GetKey("q")) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (Input.GetKey("w")) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	void OnCollisionEnter (Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Ground") || collisionInfo.gameObject.CompareTag("Brick")) {
			jumpAgain = true;
		} else if (collisionInfo.gameObject.CompareTag ("award")) {
			collisionInfo.gameObject.SetActive (false);
			score += 10;
			scoreText.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
		} else if (collisionInfo.gameObject.CompareTag("MinionObstacle")) {
			FindObjectOfType<JumpGameManagement>().EndGame();
		}

	}

	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Brick")) {
			jumpAgain = true;
		}
	}
}
