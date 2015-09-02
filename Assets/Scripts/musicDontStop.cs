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
		}
	}

	/*public GameObject musicPlayer;

	void Awake() {
		musicPlayer = GameObject.Find ("BackgroundMusic");
		if (musicPlayer == null) {
			musicPlayer = this.gameObject;
			musicPlayer.name = "BackgroundMusic";
			DontDestroyOnLoad (musicPlayer);
		} else {
		if (this.gameObject.name!="BackgroundMusic"){
				Destroy(this.gameObject);}
		}
	
	}*/

}
