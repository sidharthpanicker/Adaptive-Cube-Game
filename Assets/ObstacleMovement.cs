using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

	public Rigidbody rb;

	public float backwardForce = 2000f;

	void Start () {
		//rb.useGravity = false;
		//rb.AddForce(0,200,500);
	}

	// Update is called once per frame
	void FixedUpdate () {
		backwardForce = PlayerAdaptiveInfo.adaptiveInfo.currSpeed;
		rb.AddForce (0, 0, -backwardForce*Time.deltaTime);
	}
}
