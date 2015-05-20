using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RhythmScore : MonoBehaviour {

	public static int multiplierStreak = 7;
	public static int stdScore = 50;
	static int[] zoneMult = { -1, 2, -1};

	ShakeDetector shake;
	BoxCollider2D [] colliders;
	bool [] zones;
	HealthManager salud;
	GameObject current;

	public Text score;
	public Text mult;
	

	private int _scoreValue;
	private int multiplier;
	private int _streak;

	// Use this for initialization
	void Start () {
		shake = GetComponent<ShakeDetector> ();

		colliders = GetComponentsInChildren<BoxCollider2D> ();
		zones = new bool[3];
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

	// Update is called once per frame
	void Update () {
		if (shake.Shaking ()) {
			for( int i =0; i < zones.Length; i++ )
				if(zones[i]){
					IncreaseScore();
					Destroy(current);
					return;
				}
			LoseStreak();
			Handheld.Vibrate();
		}
	}

	public void LoseStreak(){
		streak = 0;
	}
	
	public void IncreaseScore(){
		int temp = stdScore * multiplier;
		int mt = 0;

		for (int i = 0; i < zones.Length; i++) {
			if (zones[i])
				mt += zoneMult[i];
		}
		mt = Mathf.Abs (mt);

		scoreValue = temp * mt;

		streak += 1;
	}

	public void activate( int id, GameObject g ){
		zones [id] = true;
		current = g;
	}

	public void deactivate( int id ){
		zones [id] = false;
	}


}
