using UnityEngine;
using System.Collections;

public class bloodDurability : MonoBehaviour {

	public float durability = 0.1f;

	// Update is called once per frame
	void Update () {

		durability -= Time.deltaTime;

		if (durability <= 0)
			Destroy (gameObject);
	
	}
}
