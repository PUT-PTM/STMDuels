using UnityEngine;
using System.Collections;

public class healthScript : MonoBehaviour {

	public int hp = 10;

	void OnTriggerEnter2D(){
			
		hp -= 1;

		if (hp <= 0)
			Destroy (gameObject);

	}
}
