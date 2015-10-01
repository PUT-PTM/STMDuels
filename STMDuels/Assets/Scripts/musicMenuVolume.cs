using UnityEngine;
using System.Collections;

public class musicMenuVolume : MonoBehaviour {

    private AudioSource bgMusic;
    public GameObject musicBackground;

    void Start()
    {

        //bgMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        bgMusic = musicBackground.gameObject.GetComponent<AudioSource>();

    }

    void Update()
    {

        bgMusic.volume = checkBox.musicVolume;

    }
}
