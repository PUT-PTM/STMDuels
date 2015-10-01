using UnityEngine;
using System.Collections;

public class healthScript : MonoBehaviour {

	public int hp = 10;
	public GameObject bloodSplatter;
	public GameObject Corpse;
	public GameObject Executed;

	void OnTriggerEnter2D(){
			
		Instantiate (bloodSplatter, transform.position, transform.rotation);

		hp -= 1;

		if (hp <= 0) {
			Instantiate (Corpse, transform.position, transform.rotation);
			Destroy (gameObject);
			Instantiate (Executed, transform.position, transform.rotation);
		}
	}
}
