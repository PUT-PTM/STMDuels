using UnityEngine;
using System.Collections;

public class backgroundScroll : MonoBehaviour {

	// Prędkość przesuwania tła
	public float speed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Przesuwanie tła w poziomie
		Vector2 offset = new Vector2 (Time.time * speed, 0);

		gameObject.GetComponent<Renderer> ().material.mainTextureOffset = offset;
	
	}
}
