using UnityEngine;
using System.Collections;

public class playerExecuted : MonoBehaviour {

	public float durability = 1f;

	// Update is called once per frame
	void Update () {

		transform.position = Random.insideUnitCircle * 0.02f;

		durability -= Time.deltaTime;

		if (durability <= 0)
			Destroy (gameObject);
	
	}
}
