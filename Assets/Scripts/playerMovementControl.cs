using UnityEngine;
using System.Collections;

public class playerMovementControl : MonoBehaviour {
	public float speed = 1f;
	public KeyCode moveUp;
	public KeyCode moveDown;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode shootKey;
	public int playerDirection;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (moveRight)) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
			playerDirection = 1;
		} else if (Input.GetKey (moveLeft)) {
			transform.position -= new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
			playerDirection = 2;
		} else if (Input.GetKey (moveUp)) {
			transform.position += new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);
			playerDirection = 3;
		} else if (Input.GetKey (moveDown)) {
			transform.position -= new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);
			playerDirection = 4;
		}
		
		/* bool shoot = Input.GetKeyDown (shootKey);
		
		if (shoot) {
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				weapon.Attack(false, false);
			}
		}*/
	}
}
