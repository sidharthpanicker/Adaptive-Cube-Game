using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

	public void LoadNextLevel(){
		Debug.Log ("loading Next level");
		PlayerAdaptiveInfo.IncreaseSpeed ();
		PlayerAdaptiveInfo.resetRestarts ();
		PlayerAdaptiveInfo.setCurrLevel (SceneManager.GetActiveScene ().buildIndex + 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
	}

}
