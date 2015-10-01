using UnityEngine;
using System.Collections;

public class pauseMenu : MonoBehaviour {

    public GUIStyle resumeButton;
    public GUIStyle exitButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI ()
    {

        if (GUI.Button(new Rect(Screen.width/2 - (238 * Screen.width * 0.01f * 0.05f)/2, Screen.height/2 - (63 * Screen.width * 0.01f * 0.05f) / 1.5f, 238 * Screen.width * 0.01f * 0.05f, 63 * Screen.width * 0.01f * 0.05f), "", resumeButton))
        {
            
        }

        if (GUI.Button(new Rect(Screen.width / 2 - (238 * Screen.width * 0.01f * 0.05f) / 2, Screen.height / 2 + (63 * Screen.width * 0.01f * 0.05f) / 1.5f, 238 * Screen.width * 0.01f * 0.05f, 63 * Screen.width * 0.01f * 0.05f), "", exitButton))
        {

        }

    }
}
