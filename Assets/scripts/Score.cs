using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour {

	public Transform player;
	public Text scoreText;
	public GameManager gameManager;
	// Use this for initialization
	float timer = 15;

	// Update is called once per frame
	void Update () {
		timer = timer - Time.deltaTime;
		//scoreText.text = player.position.z.ToString("0");
		if (timer < 0) {
			gameManager.CompleteLevel();
		}else if (timer <= 5f) {
			scoreText.color = Color.red;
			StartCoroutine (BlinkText (true));
			//scoreText.color = new Color(scoreText.color.r,scoreText.color.g,scoreText.color.b, Mathf.Sin(Time.time *2));
		} else {
			scoreText.text = "" + timer.ToString ("0");
		}
	}

	public IEnumerator BlinkText(bool isBlink){
		//blink it forever. You can set a terminating condition depending upon your requirement
		while(true){
			//set the Text's text to blank
			scoreText.text= "";
			//display blank text for 0.5 seconds
			yield return new WaitForSeconds(.5f);
			//display “I AM FLASHING TEXT” for the next 0.5 seconds
			if (timer < 0) {
				scoreText.text = "Loading Level "+(SceneManager.GetActiveScene().buildIndex+1);
			} else {
				scoreText.text = timer.ToString ("0");
			}
			yield return new WaitForSeconds(.5f);
		}
	}
}

