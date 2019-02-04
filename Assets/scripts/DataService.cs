using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

	public void CreateDB(){
		Debug.Log("Create DB called ");
		_connection.DropTable<User> ();
		_connection.CreateTable<User> ();
		_connection.DropTable<Session> ();
		_connection.CreateTable<Session> ();
		_connection.DropTable<Cordinates> ();
		_connection.CreateTable<Cordinates> ();
		_connection.DropTable<Attempts> ();
		_connection.CreateTable<Attempts> ();

	}

	public IEnumerable<User> GetUsers(){
		return _connection.Table<User>();
	}

	public IEnumerable<Session> GetSessions(){
		return _connection.Table<Session>();
	}

	public User CreateGivenUser(User p){
		User tmp = null;
		tmp = _connection.Table<User>().Where(x => x.Name == p.Name).FirstOrDefault();
		if (tmp == null){
			_connection.Insert(p);
			tmp = _connection.Table<User>().Where(x => x.Name == p.Name).FirstOrDefault();
		}
		return tmp;
	}

	public Session CreateGivenSession(Session s){
		_connection.Insert(s);
		var m = QueryValuations(_connection,s);
		foreach (var per in m) {
			return per;
		}
		return s;
	}

	public Cordinates CreateGivenCordinates(Cordinates cord){
		_connection.Insert(cord);
		return cord;
	}

	public Attempts CreateGivenAttempt(Attempts attmpt){
		_connection.Insert(attmpt);
		return attmpt;
	}

	public static IEnumerable<Session> QueryValuations(SQLiteConnection db, Session sess)		
	{		
    	return db.Query<Session> ("select * from Session where UserID = ? order by SessionId desc", sess.UserID);		
	}
}
