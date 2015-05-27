using UnityEngine;

public class WeaponScript : MonoBehaviour
{
	// model strzału
	public Transform shotPrefab;
	
	// opóźnienie między strzałami
	public float shootingRate = 0.25f;
	
	private float shootCooldown;

	private PlayerControl zmienne;

	private int direc;
	
	void Start()
	{
		shootCooldown = 0f;
		zmienne = GetComponent<PlayerControl> ();
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}

		if (Input.GetKeyDown (zmienne.moveUp))
			direc = 1;
		else if (Input.GetKeyDown (zmienne.moveDown))
			direc = 2;
		else if (Input.GetKeyDown (zmienne.moveLeft))
			direc = 3;
		else if (Input.GetKeyDown (zmienne.moveRight))
			direc = 4;
	}
	
	// funkcja strzału. do użycia z innego skryptu
	public void Attack(bool isWall, bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;
			
			// Tworzenie nowego strzału
			var shotTransform = Instantiate(shotPrefab) as Transform;
			
			// Przypisanie pozycji
			shotTransform.position = transform.position;

			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.isWallShot = isWall;
				shot.isEnemyShot = isEnemy;
			}
			
			// Ustawianie kierunku strzału
			BulletMovement move = shotTransform.gameObject.GetComponent<BulletMovement>();
			if (move != null)
			{
				if (direc == 1)
					move.direction = this.transform.up;
				else if (direc == 2)
					move.direction = -this.transform.up;
				else if (direc == 3)
					move.direction = -this.transform.right;
				else if (direc == 4)
					move.direction = this.transform.right;
			}
		}
	}

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}