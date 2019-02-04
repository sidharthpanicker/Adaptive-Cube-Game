using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDbScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartSync();
	}
	
	private void StartSync()
    {
        //var ds = new DataService("tempDatabase.db");
        //ds.CreateDB();
        
		//ds.CreateUser();
        //var person = ds.GetUsers();
        //ToConsole (person);
    }
	
	private void ToConsole(IEnumerable<User> person){
		foreach (var per in person) {
			ToConsole(per.ToString());
		}
	}
	
	private void ToConsole(string msg){
		Debug.Log (msg);
	}
}
