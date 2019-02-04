using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;

	public float forwardForce = 2000f;
	public float sidewaysForce = 500f;
	// Use this for initialization
	void Start () {
		//rb.useGravity = false;
		//rb.AddForce(0,200,500);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//rb.AddForce (0, 0, forwardForce*Time.deltaTime);
		sidewaysForce = PlayerAdaptiveInfo.adaptiveInfo.sideForce;
		if (Input.GetKey ("d")) {
			rb.AddForce (sidewaysForce*Time.deltaTime,0,0, ForceMode.VelocityChange);
		}
		if (Input.GetKey ("a")) {
			rb.AddForce (-sidewaysForce*Time.deltaTime,0,0,ForceMode.VelocityChange);
		}
		//PlayerStats.AddCordinates (rb.position.x,rb.position.y,rb.position.z);
		if (rb.position.y < -1f) {
			if (rb.position.x < 0) {
				FindObjectOfType<GameManager> ().EndGame (GameManager.LEFT_SIDE_FALL);
			} else {
				FindObjectOfType<GameManager> ().EndGame (GameManager.RIGHT_SIDE_FALL);
			}
		}
	}
}
