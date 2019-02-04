using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	bool isGameEnded = false;
	public float restartDelay = 2f;
	public GameObject completeLevelUI;
	public User userObject;
	public const string RIGHT_SIDE_FALL = "RIGHT_SIDE_FALL";
	public const string LEFT_SIDE_FALL = "LEFT_SIDE_FALL";
	public const string COLLISION = "COLLISION";
	public const int NUMBER_OF_TIMES_ALLOWED_TO_ADAPT = 1;
	public const int NUMBER_OF_ALLOWED_RESTARTS = 3;
	public const int NUMBER_OF_ALLOWED_COLLISIONS = 2;

	public void setUser(User user){
		userObject = user;
	} 

	public void CreateSession(){
		var session = new Session{
				UserID = userObject.Id
		};
		var ds = new DataService("tempDatabase.db");
		Session tmp = ds.CreateGivenSession(session);
		Debug.Log(tmp.ToString());
	} 

	public void CompleteLevel(){
		Debug.Log ("Level complete");
		PlayerStats.incrementLevel();
		completeLevelUI.SetActive (true);
	}

	public void EndGame(string reason){
		
		if (!isGameEnded) {
			Debug.Log ("End reason : " + reason);
			PlayerAdaptiveInfo.IncreaseRestartCount ();
			Attempts attempt = new Attempts ();
			attempt.Completed = false;
			attempt.FailureType = reason;
			attempt.Level = PlayerAdaptiveInfo.getCurrLevel ();
			attempt.SessionId = PlayerStats.SessionDetails.SessionId;
			attempt.SideForce = PlayerAdaptiveInfo.adaptiveInfo.sideForce;
			attempt.Speed = PlayerAdaptiveInfo.adaptiveInfo.currSpeed;
			PlayerStats.AddAttemptData (attempt);
			if (reason.Equals (COLLISION)) {
				PlayerAdaptiveInfo.IncreaseCollisionCount ();
			}
			PlayerAdaptiveInfo.adjustSpeedOrLevel ();
			Debug.Log ("Game Over");
			isGameEnded = true;
			Invoke ("Restart",restartDelay);
			//Restart ();
		}
	}

	void Restart(){
		int level = PlayerAdaptiveInfo.getCurrLevel ();
		//SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		SceneManager.LoadScene (level);
	}
}
