using UnityEngine;
using System.Collections;

public class musicDontStop : MonoBehaviour {

	public GameObject thisAudio;

	static bool audioBegin = false;

	void Awake () {
		if (!audioBegin) {
			thisAudio.GetComponent<AudioSource> ().Play ();
			DontDestroyOnLoad (gameObject);
			audioBegin = true;
		}
	}

	void Update () {
		if(Application.loadedLevelName == "levelGarden")
		{
			Destroy(thisAudio);
            audioBegin = false;
		}
	}
}
