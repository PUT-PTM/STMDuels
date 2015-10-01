using UnityEngine;
using System.Collections;

public class Winner : MonoBehaviour {

    public static int alive;

    // Update is called once per frame

    IEnumerator goMenu()
    {
        yield return new WaitForSeconds(5);
        float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(0);
    }

    void Start ()
    {

        alive = playerSpawn.howMany;

    }


	void Update () {

        if (alive == 0)
            StartCoroutine(goMenu());
	
	}
}
