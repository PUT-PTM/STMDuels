using UnityEngine;
using System.Collections;

public class playerAnimationDirection : MonoBehaviour {
	
	Animator anim;
	playerMovementControl zmienne;
	
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
		zmienne = GetComponent<playerMovementControl> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (zmienne.moveUp))
			anim.SetInteger ("direction", 1);
		else if (Input.GetKeyDown (zmienne.moveDown))
			anim.SetInteger ("direction", 4);
		else if (Input.GetKeyDown (zmienne.moveLeft))
			anim.SetInteger ("direction", 2);
		else if (Input.GetKeyDown (zmienne.moveRight))
			anim.SetInteger ("direction", 3);
		
	}
}