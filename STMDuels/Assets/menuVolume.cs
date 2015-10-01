using UnityEngine;
using System.Collections;

public class menuVolume : MonoBehaviour {

    public GameObject musicBackground;

    void Start()
    {

        
       
       musicBackground.gameObject.GetComponent<AudioSource>().Play();
        
    }


    void Update ()
    {

        musicBackground.gameObject.GetComponent<AudioSource>().volume = 0.0f;

    }
    // Use this for initialization

}
