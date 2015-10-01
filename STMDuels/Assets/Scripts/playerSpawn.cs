using UnityEngine;
using System.Collections;

public class playerSpawn : MonoBehaviour {

	public GameObject character;
	public int playerNumber;
	public GameObject staticReady;
    public static int howMany = 0;
	private arePlayersReady readyScript;

	// Use this for initialization
	void Start () {

		readyScript = staticReady.gameObject.GetComponent<arePlayersReady> ();

		readyScript.isReady ();

        if (playerNumber == 1)
            if (readyScript.player1ready == true)
            {
                Instantiate(character, transform.position, transform.rotation);
                howMany++;
            }
        if (playerNumber == 2)
            if (readyScript.player2ready == true)
            {
                Instantiate(character, transform.position, transform.rotation);
                howMany++;
            }
        if (playerNumber == 3)
            if (readyScript.player3ready == true)
            {
                Instantiate(character, transform.position, transform.rotation);
                howMany++;
            }
        if (playerNumber == 4)
            if (readyScript.player4ready == true)
            {
                Instantiate(character, transform.position, transform.rotation);
                howMany++;
            }
	}

}
