using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public int damage = 10;

	public bool isWallShot = false;
	public bool isEnemyShot = false;

	// Use this for initialization
	void Start () {

		Destroy (gameObject, 20);
	
	}
}
