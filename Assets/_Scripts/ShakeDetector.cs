using UnityEngine;
using System.Collections;

public class ShakeDetector : MonoBehaviour {
	
	public float peakLevel = 1.75f;
	public float delta = 0.2f;
	public float deltaTime = 0.05f;

	float timer = 0.0f;
	float lastMag = 0.0f;

	public bool Shaking(){
		Vector3 curAcc = Input.acceleration; // tomar la aceleracion actual

		// verificar si se pasa el threshold
		if (curAcc.magnitude > peakLevel) {

			if ( curAcc.magnitude > lastMag+delta || curAcc.magnitude < lastMag-delta ){
				timer = 0.0f;
				lastMag = curAcc.magnitude;
				return true;
			}
			if (timer>deltaTime){
				timer = 0.0f;
				lastMag = curAcc.magnitude;
				return true;
			}
			timer += Time.deltaTime;
		}
		return false;
	}
}
