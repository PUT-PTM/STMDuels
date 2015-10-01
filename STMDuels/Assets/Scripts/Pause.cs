using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public bool isPause = false;
    public bool showGUI = false;
    public GameObject PauseGUI;
    public GameObject Cursor;
    public GUIStyle exitButton;
    public GUIStyle resumeButton;
    public GameObject musicVolume;
    private AudioSource bgMusic;

    IEnumerator ExitGame()
    {
        float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(0);
    }

    void Start ()
    {

        PauseGUI.SetActive(false);
        Cursor.SetActive(false);
        bgMusic = musicVolume.gameObject.GetComponent<AudioSource>();

    }

	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown("p"))
        {

            isPause = !isPause;

        }

        if (isPause == true)
        {

            Time.timeScale = 0;
            bgMusic.volume = 0.05f * checkBox.musicVolume;
            showGUI = true;

        }

        if (isPause == false)
        {

            Time.timeScale = 1;
            bgMusic.volume = checkBox.musicVolume;
            showGUI = false;

        }

        if (showGUI == true)
        {
            PauseGUI.SetActive(true);
            Cursor.SetActive(true);
        }

        if (showGUI == false)
        {
            PauseGUI.SetActive(false);
            Cursor.SetActive(false);
        }

	}


    void OnGUI()
    {

        if (isPause == true)
        {

            if (GUI.Button(new Rect(Screen.width / 2 - (238 * Screen.width * 0.01f * 0.05f) / 2, Screen.height / 2 - (63 * Screen.width * 0.01f * 0.05f) / 1.5f, 238 * Screen.width * 0.01f * 0.05f, 63 * Screen.width * 0.01f * 0.05f), "", resumeButton))
            {

                isPause = false;

            }

            if (GUI.Button(new Rect(Screen.width / 2 - (238 * Screen.width * 0.01f * 0.05f) / 2, Screen.height / 2 + (63 * Screen.width * 0.01f * 0.05f) / 1.5f, 238 * Screen.width * 0.01f * 0.05f, 63 * Screen.width * 0.01f * 0.05f), "", exitButton))
            {

                isPause = false;
                StartCoroutine(ExitGame());

            }

        }

    }
}
