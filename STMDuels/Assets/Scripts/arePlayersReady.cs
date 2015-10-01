using UnityEngine;
using System.Collections;

public class arePlayersReady : MonoBehaviour {

	public static bool player1go = false;
	public static bool player2go = false;
	public static bool player3go = false;
	public static bool player4go = false;
	public bool player1ready = false;
	public bool player2ready = false;
	public bool player3ready = false;
	public bool player4ready = false;

	public GameObject selectionObject;
	private playerSelection readiness;


	// Use this for initialization
	void Awake () {
		readiness = selectionObject.gameObject.GetComponent<playerSelection> ();
		DontDestroyOnLoad(transform.gameObject);
	}

	public void getReady() {
		player1go = readiness.player1go;
		player2go = readiness.player2go;
		player3go = readiness.player3go;
		player4go = readiness.player4go;
	}

	public void isReady() {
		player1ready = player1go;
		player2ready = player2go;
		player3ready = player3go;
		player4ready = player4go;
	}
}
