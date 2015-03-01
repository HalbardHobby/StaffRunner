using UnityEngine;
using System.Collections;

public class CubeSurface : MonoBehaviour 
{
	public Color mainColor ;

	private Material material;

	void Start () 
	{
		material = renderer.material = renderer.material;
	}
	
	void Update () 
	{
		material.color = mainColor;
	}
}
