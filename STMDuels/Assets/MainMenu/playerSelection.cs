using UnityEngine;
using System;
using System.Collections;

public class playerSelection : MonoBehaviour {

	public Texture player1nr;
	public Texture player2nr;
	public Texture player3nr;
	public Texture player4nr;
	public Texture player1r;
	public Texture player2r;
	public Texture player3r;
	public Texture player4r;
	public KeyCode player1;
	public KeyCode player2;
	public KeyCode player3;
	public KeyCode player4;
	public bool player1go = false;
	public bool player2go = false;
	public bool player3go = false;
	public bool player4go = false;
	public GUIStyle goBack;
	public GUIStyle goNext;
	
	public GameObject staticReady;
	private arePlayersReady readyScript;
    private int i;

	IEnumerator goMenu () {
		float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(0);
	}

	IEnumerator goPlay () {
		float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(2);
	}

	// Use this for initialization
	void Start () {
		readyScript = staticReady.gameObject.GetComponent<arePlayersReady> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (player1)) {
			if (player1go == false)
				player1go = true;
			else if (player1go == true)
				player1go = false;
		} 
		if (Input.GetKeyDown (player2)) {
			if (player2go == false)
				player2go = true;
			else if (player2go == true)
				player2go = false;
		}
		if (Input.GetKeyDown (player3)) {
			if (player3go == false)
				player3go = true;
			else if (player3go == true)
				player3go = false;
		}
        if (Input.GetKeyDown(player4))
        {
			if (player4go == false)
				player4go = true;
			else if (player4go == true)
				player4go = false;
		}
	}

	void OnGUI () {
	
		if (player1go == false)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 - 0.2f * Screen.width, 0.1f * Screen.height, 224, 277), player1nr);
		else if (player1go == true)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 - 0.2f * Screen.width, 0.1f * Screen.height, 224, 277), player1r);

		if (player2go == false)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 + 0.2f * Screen.width, 0.1f * Screen.height, 224, 277), player2nr);
		else if (player2go == true)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 + 0.2f * Screen.width, 0.1f * Screen.height, 224, 277), player2r);

		if (player3go == false)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 - 0.2f * Screen.width, Screen.height - 227 - 0.15f * Screen.height, 224, 277), player3nr);
		else if (player3go == true)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 - 0.2f * Screen.width, Screen.height - 227 - 0.15f * Screen.height, 224, 277), player3r);

		if (player4go == false)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 + 0.2f * Screen.width, Screen.height - 227 - 0.15f * Screen.height, 224, 277), player4nr);
		else if (player4go == true)
			GUI.DrawTexture (new Rect (Screen.width / 2 - 112 + 0.2f * Screen.width, Screen.height - 227 - 0.15f * Screen.height, 224, 277), player4r);

		if (GUI.Button (new Rect(90, 90, 100, 100), "", goBack)) {
			player1go = false; player2go = false; player3go = false; player4go = false;
			StartCoroutine(goMenu());
		}

		if (GUI.Button (new Rect(Screen.width - 190, 90, 100, 100), "", goNext)) {
			readyScript.getReady();
			StartCoroutine(goPlay());
		}
        
	}


   
}
