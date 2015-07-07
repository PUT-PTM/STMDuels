using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public float speed = 1f;


	public KeyCode moveUp;
	public KeyCode moveDown;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode shootKey;

	public STMReceiver SterowanieSTM;

	// Use this for initialization
	void Start () {
	
		SterowanieSTM = new STMReceiver();
		SterowanieSTM.StartListening();

	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKey (moveRight))
			transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
		else if (Input.GetKey (moveLeft))
			transform.position -= new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
		else if (Input.GetKey (moveUp))
			transform.position += new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);
		else if (Input.GetKey (moveDown))
			transform.position -= new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);
		*/
		bool shoot = SterowanieSTM.is_shooting;


		if (SterowanieSTM.axisZ<40.0f&&SterowanieSTM.axisX<1.0f)
			transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
		else if (SterowanieSTM.axisX>30.0f)
			transform.position -= new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
		else if (SterowanieSTM.axisY<5.0f&&SterowanieSTM.axisX<10.0f&&SterowanieSTM.axisZ<40.0f)
			transform.position += new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);
		else if (SterowanieSTM.axisY>20.0f&&SterowanieSTM.axisY<60.0f&&SterowanieSTM.axisX<10.0f&&SterowanieSTM.axisZ>40.0f)
			transform.position -= new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);

		if (shoot) {
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				weapon.Attack(false, false);
			}
		}
	}

	void OnDestroy()
	{
		if (SterowanieSTM != null)
		{
			SterowanieSTM.Dispose();
		}
	}
}
