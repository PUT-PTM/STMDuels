using UnityEngine;
using System.Collections;

public class playerSpawn : MonoBehaviour {

	public GameObject character;

	// Use this for initialization
	void Start () {
		Instantiate (character, transform.position, transform.rotation);	
	}

}
