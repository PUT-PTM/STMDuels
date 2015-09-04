using UnityEngine;
using System.Collections;

public class bulletCollider : MonoBehaviour {

	void OnTriggerEnter2D() {
		Debug.Log ("Trigger!");
		bulletDestroy ();
	}
	
	// Update is called once per frame
	void bulletDestroy () {
		Destroy (gameObject);
	}
}
