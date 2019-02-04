using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour {

	public Transform[] spawnPoints;

	int[] blocks= {1,2,3,4,5};
	public GameObject blockPreFab;
	public float rateOfSpawn = 1f;

	private float timeToSpawn=2f;

	void Update () {
		rateOfSpawn = PlayerAdaptiveInfo.adaptiveInfo.rateOfSpawn;
		if (Time.time >= timeToSpawn) {
			spawnBlocks ();
			timeToSpawn = Time.time + rateOfSpawn;
		}

	}

	void spawnBlocks(){
		int level = SceneManager.GetActiveScene ().buildIndex;
		HashSet<int> randomIndices = new HashSet<int>();
		if (PlayerAdaptiveInfo.isStrictModeOn ()) {
			while(randomIndices.Count<blocks[level]){
				int randomIndex = Random.Range (0, spawnPoints.Length);
				randomIndices.Add (randomIndex);
			}
		} else {
			for (int i = 0; i < blocks [level]; i++) {
				int randomIndex = Random.Range (0, spawnPoints.Length);
				randomIndices.Add (randomIndex);
			}
		}

		Debug.Log ("at indices " + randomIndices.Count +" level : " + level);


		for (int i = 0; i < spawnPoints.Length; i++) {
			if (randomIndices.Contains(i)){
				Instantiate (blockPreFab, spawnPoints[i].position, Quaternion.identity);
			}
		}
		randomIndices.Clear ();
	}
	

}
