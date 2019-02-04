using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public static class PlayerStats
{
    private static User user;
    private static Session session;
    private static int Lev = 1;
    
    public static User UserDetails 
    {
        get 
        {
            return user;
        }
        set 
        {
            user = value;
        }
    }

    public static Session SessionDetails 
    {
        get 
        {
            return session;
        }
        set 
        {
            session = value;
        }
    }

    public static void incrementLevel(){
    	Lev = Lev+1;
    }

    public static void CreateSession(){
		var sess = new Session{
				UserID = user.Id
		};
		var ds = new DataService("tempDatabase.db");
		session = ds.CreateGivenSession(sess);
		//Debug.Log("Session ID:"+session.ToString());
	} 

	public static void AddCordinates(float x, float y, float z){
		System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
 		float cur_time = (float)(System.DateTime.UtcNow - epochStart).TotalSeconds;
		var cord = new Cordinates{
				X_Cord = x,
				Y_Yord = y,
				Z_Cord = z,
				Time = cur_time,
				SessionId = session.SessionId,
				Level = SceneManager.GetActiveScene().buildIndex
		};
		var ds = new DataService("tempDatabase.db");
		ds.CreateGivenCordinates(cord);
		//Debug.Log("Session ID:"+session.ToString());
	} 

	public static void AddAttemptData(Attempts attempt){
		System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
 		float cur_time = (float)(System.DateTime.UtcNow - epochStart).TotalSeconds;
		// TO BE IMPLEMENTED
		//var attmpt = new Attempts{};
		var ds = new DataService("tempDatabase.db");
		ds.CreateGivenAttempt(attempt);
		
	} 

}
