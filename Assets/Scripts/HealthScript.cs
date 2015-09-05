using UnityEngine;
using System.Collections;

public class healthScript : MonoBehaviour {

	public int hp = 10;
	public GameObject bloodSplatter;

	void OnTriggerEnter2D(){
			
		Instantiate (bloodSplatter, transform.position, transform.rotation);

		hp -= 1;

		if (hp <= 0)
			Destroy (gameObject);

	}
}
