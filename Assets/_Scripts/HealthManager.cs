using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public Slider salud;
	public float damage = 10f;
	public float recover = 7.5f;

	public RectTransform GameOverScreen;
	public RectTransform main;
	public NotesManager spawn;

	private float health{
		get{
			return salud.value;
		}
		set{
			salud.value = value;
		}
	}
	
	void Start(){
		health = 50f;
	}

	public void Damage(){
		health -= damage;
		if (health <= 0)
			GameOver ();
	}

	public void Recover(){
		health += recover;
	}

	public void GameOver(){
		spawn.CancelInvoke ("Spawn");
		spawn.CancelInvoke ("ChangeChord");
		RectTransform rect = Instantiate (GameOverScreen) as RectTransform;
		rect.SetParent (main, false);
		Text fin = rect.FindChild("FinalScore").GetComponent<Text>();
		ScoreManager score = GetComponent<ScoreManager> ();
		fin.text = "Loading Scores";

		StartCoroutine (PostScores("User",score.scoreValue));
		StartCoroutine (GetScores());
	}

	//codigo de manejo online.
	private string secretKey = "sr_key"; // Edit this value and make sure it's the same as the one stored on the server
	public string addScoreURL = "http://104.131.163.157/addscore.php?"; //be sure to add a ? to your url
	public string highscoreURL = "http://104.131.163.157/display.php";

	IEnumerator PostScores(string name, int score)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = Md5Sum(name + score + secretKey);
		
		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
		
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}
	
	// Get the scores from the MySQL DB to display in a GUIText.
	// remember to use StartCoroutine when calling this function!
	IEnumerator GetScores()
	{
		RectTransform rect = Instantiate (GameOverScreen) as RectTransform;
		rect.SetParent (main, false);
		Text fin = rect.FindChild("FinalScore").GetComponent<Text>();

		WWW hs_get = new WWW(highscoreURL);
		yield return hs_get;
		
		if (hs_get.error != null)
		{
			print("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			fin.text = "HighScores: "+ hs_get.text; // this is a GUIText that will display the scores in game.

		}
	}


	public  string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
}
