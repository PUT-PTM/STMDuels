using UnityEngine;

public class BulletMovement : MonoBehaviour
{

	// Prędkość pocisku
	public Vector2 speed = new Vector2(10, 10);
	
	// Kierunek pocisku
	public Vector2 direction = new Vector2(0, 1);
	
	private Vector2 movement;

	private Rigidbody2D myRigidBody;

	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
	}
	
	void FixedUpdate()
	{
		myRigidBody.velocity = movement;
	}
}