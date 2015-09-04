using UnityEngine;
using System.Collections;

public class playerShooting : MonoBehaviour {

	public GameObject bulletPrefab;

	public KeyCode shootKey;

	playerMovementControl varDir;

	public float fireDelay = 0.25f;
	float cooldownTimer = 0;

	void Start () {
		varDir = GetComponent<playerMovementControl> ();
	}

	void Update () {
		cooldownTimer -= Time.deltaTime;

		if(Input.GetKeyDown(shootKey) && cooldownTimer <= 0) {

			cooldownTimer = fireDelay;

			if (varDir.playerDirection == 3)
				Instantiate(bulletPrefab, transform.position, transform.rotation);
			else if (varDir.playerDirection == 4) 
				Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0,0,180));
			else if (varDir.playerDirection == 2)
				Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0,0,90));
			else if (varDir.playerDirection == 1)
				Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0,0,270));
			else 
				Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0,0,180));
	}
}
}
