using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class PlayerAdaptiveInfo : MonoBehaviour {

	public static PlayerAdaptiveInfo adaptiveInfo;
	public const float SPEED_INCREASE_DECREASE = 50f;
	public const float SIDE_FORCE_INCREASE_DECREASE = 10f;
	public int currLevel;
	public int noOfRestarts;
	public float currSpeed;
	public bool isStrictMode;
	public float sideForce;
	public float rateOfSpawn;
	public int noOfTimesAdapted;
	public int noOfCollisions;
	public string userName;
	// Use this for initialization
	void Awake () {
		if (adaptiveInfo == null) {
			adaptiveInfo = this;
			DontDestroyOnLoad (gameObject);
		} else if(adaptiveInfo !=this){
			Destroy (gameObject);
		}

	}

	public static int getCurrLevel(){
		return adaptiveInfo.currLevel;
	}
	// Update is called once per frame
	public void Save () {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/adaptiveInfo" + userName + ".dat",FileMode.Open);
		AdaptiveData ad = new AdaptiveData ();
		ad.currLevel = currLevel;
		ad.currSpeed = currSpeed;
		ad.isStrictMode = isStrictMode;
		ad.noOfRestarts = noOfRestarts;
		ad.rateOfSpawn = rateOfSpawn;
		ad.sideForce = sideForce;
		ad.userName = userName;
		bf.Serialize (file, ad);
		file.Close ();
	}

	public static void IncreaseSpeed(){
		Debug.Log ("Increasing current Speed " + adaptiveInfo.noOfRestarts);
		if (adaptiveInfo.noOfRestarts == 0) {
			adaptiveInfo.currSpeed = adaptiveInfo.currSpeed + 3 * SPEED_INCREASE_DECREASE;
			adaptiveInfo.isStrictMode = true;
		}else if (adaptiveInfo.noOfRestarts <= 2) {
			adaptiveInfo.currSpeed = adaptiveInfo.currSpeed + SPEED_INCREASE_DECREASE;
			adaptiveInfo.isStrictMode = false;
		}else {
			adaptiveInfo.currSpeed = adaptiveInfo.currSpeed - SPEED_INCREASE_DECREASE;
			adaptiveInfo.isStrictMode = false;
		}
	}

	public static void resetRestarts(){
		adaptiveInfo.noOfRestarts = 0;
		adaptiveInfo.noOfCollisions = 0;
	}
	public static void DecreaseSpeed(){
		adaptiveInfo.currSpeed = adaptiveInfo.currSpeed - SPEED_INCREASE_DECREASE;
		//adaptiveInfo.isStrictMode = false;
	}

	public static void DecreaseSideForce(){
		adaptiveInfo.sideForce = adaptiveInfo.sideForce - SIDE_FORCE_INCREASE_DECREASE;
		//adaptiveInfo.isStrictMode = false;
	}


	public static void IncreaseSideForce(){
		adaptiveInfo.sideForce = adaptiveInfo.sideForce + SIDE_FORCE_INCREASE_DECREASE;
	}

	public static void adjustSpeedOrLevel(){
		Debug.Log (" calling adjust speed or level");
		if (adaptiveInfo.noOfTimesAdapted < GameManager.NUMBER_OF_TIMES_ALLOWED_TO_ADAPT) {
			//Debug.Log (" in if ");
			if (adaptiveInfo.noOfRestarts > GameManager.NUMBER_OF_ALLOWED_RESTARTS) {
				//Debug.Log (" in if 2");
				if (adaptiveInfo.noOfCollisions > GameManager.NUMBER_OF_ALLOWED_COLLISIONS) {
					DecreaseSpeed ();
					adaptiveInfo.isStrictMode = false;
				} else {
					DecreaseSideForce ();
				}
				adaptiveInfo.noOfRestarts = 0;
				adaptiveInfo.noOfCollisions = 0;
				//Debug.Log (" calling resetting");
				adaptiveInfo.noOfTimesAdapted = adaptiveInfo.noOfTimesAdapted + 1;
			}
		} else {
			//Debug.Log (" in else ");
			if (adaptiveInfo.currLevel != 0) {
				adaptiveInfo.currLevel = adaptiveInfo.currLevel - 1;
				adaptiveInfo.isStrictMode = true;
			}
			adaptiveInfo.noOfTimesAdapted = 0;
		}
	}

	public static bool isStrictModeOn(){
		return adaptiveInfo.isStrictMode;
	}

	public static void setCurrLevel(int level){
		adaptiveInfo.noOfRestarts = 0;
		adaptiveInfo.currLevel = level;
	}

	public static void IncreaseRestartCount(){
		adaptiveInfo.noOfRestarts = adaptiveInfo.noOfRestarts + 1;
	}

	public static void IncreaseCollisionCount(){
		adaptiveInfo.noOfCollisions = adaptiveInfo.noOfCollisions + 1;
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/adaptiveInfo" + userName + ".dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/adaptiveInfo" + userName + ".dat",FileMode.Open);
			AdaptiveData ad = (AdaptiveData)bf.Deserialize(file);
			file.Close ();
			currLevel = ad.currLevel;
			currSpeed = ad.currSpeed;
			isStrictMode = ad.isStrictMode;
			noOfRestarts =ad.noOfRestarts;
			rateOfSpawn = ad.rateOfSpawn;
			sideForce = ad.sideForce;
			userName = ad.userName;
		}
	}
}
[Serializable]
class AdaptiveData{
	public int currLevel;
	public int noOfRestarts;
	public float currSpeed;
	public bool isStrictMode;
	public float sideForce;
	public float rateOfSpawn;
	public string userName;
}
