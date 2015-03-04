using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public Slider salud;
	public float damage = 10f;
	public float recover = 7.5f;
	
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
		//TODO interfaz game over
	}
}
