using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int multiplierStreak = 7;
	public static int stdScore = 50;
	
	public Text score;
	public Text mult;

	private int _scoreValue;
	private int multiplier;
	private int _streak;


	private HealthManager salud;

	
	void Awake(){
		salud = GetComponent<HealthManager>();
	}

	private int streak{
		get {
			return _streak;
		}
		set {
			_streak = value;
			multiplier = (streak / multiplierStreak)+1;

			mult.text = "X"+multiplier.ToString();
		}
	}

	public int scoreValue{
		get{
			return _scoreValue;
		}
		set{
			_scoreValue = value;
			score.text=_scoreValue.ToString();
		}
	}

	public void Start(){
		streak = 0;
		scoreValue = 0;
	}

	public void LoseStreak(){
		streak = 0;
	}

	public void IncreaseScore(){
		scoreValue += stdScore * multiplier;
		streak += 1;

		if(streak%5==0){
			salud.HeroPowerIncrease();
		}

		Debug.Log (streak);
	}
}
