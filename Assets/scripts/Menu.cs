
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public InputField username;
	public InputField Age;
	//public GameManager gameManager;

	public void StartGame(){
		Debug.Log("Name: " + username.text + "Age: " + Age.text);
		

		var p = new User{
				Name = username.text,
				Surname = "NA",
				Age = int.Parse(Age.text)
		};
		var ds = new DataService("tempDatabase.db");
		User tmp = ds.CreateGivenUser(p);
		PlayerStats.UserDetails = tmp;
		PlayerStats.CreateSession();
	
		PlayerAdaptiveInfo.adaptiveInfo.userName = username.text;
		PlayerAdaptiveInfo.setCurrLevel (SceneManager.GetActiveScene ().buildIndex + 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);

	}

	private void ToConsole(IEnumerable<User> person){
		foreach (var per in person) {
			Debug.Log(per.ToString());
		}
	}
	
}
