using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

	public int bufferSize;
	public int volume;
	public int instrument;
	public int note;
	public float duration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PlayNote(){
		yield return null;
	}
}
