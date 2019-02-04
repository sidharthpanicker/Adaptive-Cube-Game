using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
	public Vector3 offSet;
	// Update is called once per frame
	void Update () {
		//Debug.Log (player.position);
		PlayerStats.AddCordinates(player.position.x, player.position.y, player.position.z);
		transform.position = player.position+offSet;
	}
}
