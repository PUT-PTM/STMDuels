using UnityEngine;
using System;
using System.Collections;

public class playerMovementControl : MonoBehaviour {
	public float speed = 1f;
	public KeyCode moveUp;
	public KeyCode moveDown;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode shootKey;
	public int playerDirection;
	public int controlMode;
    public bool shoot;
    public Int16 comparision;
    public int i;
    
	public STMReceiver SterowanieSTM;
    
    

	// Use this for initialization
	void Start () {
		SterowanieSTM = new STMReceiver();
		SterowanieSTM.StartListening();
        comparision = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if(controlMode == 1){
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
		}
		if(controlMode == 2){
            Debug.Log("Tyle wynosi aX: " + SterowanieSTM.axisXint);
            Debug.Log("Tyle wynosi aY: " + SterowanieSTM.axisYint);
            if (SterowanieSTM.axisXint > comparision)
            {
			transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
			playerDirection = 1;
            }
            else if (SterowanieSTM.axisXint < -(comparision))
            {
			transform.position -= new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
			playerDirection = 2;
            }
            else if (SterowanieSTM.axisYint > comparision)
            {
			transform.position += new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);
			playerDirection = 3;
            }
            else if (SterowanieSTM.axisYint < -(comparision))
            {
			transform.position -= new Vector3 (0.0f, speed * Time.deltaTime, 0.0f);
			playerDirection = 4;
            if (SterowanieSTM.is_shooting) shoot = true;
            else shoot = false;
		}
		}
		
	}


}
