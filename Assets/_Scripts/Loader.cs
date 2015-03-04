using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public void load(int scene){
		Application.LoadLevel (scene);
	}
}
