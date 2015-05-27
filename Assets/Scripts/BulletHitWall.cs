using UnityEngine;
using System.Collections;

public class BulletHitWall : MonoBehaviour {

	public bool isWall = true;

	void OnTriggerEnter2D (Collider2D collider) {
	
		ShotScript shot = collider.gameObject.GetComponent<ShotScript> ();

		if (shot != null) {
			if(shot.isWallShot != isWall)
			{
				Destroy(shot.gameObject);
			}
		}

	}
}
