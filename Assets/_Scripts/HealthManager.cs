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
		fin.text = "Your final score was: " + score.scoreValue.ToString ();
	}
}
