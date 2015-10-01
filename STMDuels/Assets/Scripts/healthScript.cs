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
            Winner.alive -= 1;
            if (Winner.alive > 0)
			    Instantiate (Executed, transform.position, transform.rotation);            
		}
	}
}
