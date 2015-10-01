using UnityEngine;
using System.Collections;

public class checkBox : MonoBehaviour {

    public bool isFullScreen = true;
    public static float musicVolume = 1;
    bool isToggled = true;
    private AudioSource bgMusic;
    
    void Start ()
    {

        bgMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();

    }

    void Update ()
    {

        bgMusic.volume = musicVolume;

    }

    // Use this for initialization
    void OnGUI () {
        isFullScreen = GUI.Toggle(new Rect(400, 380, 480, 25), isFullScreen, "Graj w trybie pelnoekranowym");
        musicVolume = GUI.HorizontalSlider(new Rect(400, 410, 400, 25), musicVolume, 0.0f, 1.0f);
        if (isToggled != isFullScreen)
        {
            Screen.fullScreen = !Screen.fullScreen;
            isToggled = !isToggled;
        }
    }
}
