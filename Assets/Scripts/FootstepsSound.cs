using UnityEngine;
using System.Collections;

public class FootstepsSound : MonoBehaviour {

	public AudioSource footsteps;
	PlayerControl zmienne;

	// Use this for initialization
	void Start () {

		zmienne = GetComponent<PlayerControl> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (zmienne.moveUp) || Input.GetKeyDown (zmienne.moveDown) || Input.GetKeyDown (zmienne.moveLeft) || Input.GetKeyDown (zmienne.moveRight))
			footsteps.Play ();
		else if (Input.GetKeyUp (zmienne.moveUp) || Input.GetKeyUp (zmienne.moveDown) || Input.GetKeyUp (zmienne.moveLeft) || Input.GetKeyUp (zmienne.moveRight))
			footsteps.Stop ();
	
	}
}
