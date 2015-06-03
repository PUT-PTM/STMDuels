using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture logo;
	public GUIStyle playButton;
	public GUIStyle optionsButton;
	public GUIStyle exitButton;

	void OnGUI(){
	
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		GUI.DrawTexture (new Rect (Screen.width/2 - (820 * Screen.width * 0.01f * 0.05f)/2, Screen.height * 0.05f, 820 * Screen.width * 0.01f * 0.05f, 401 * Screen.width * 0.01f * 0.05f), logo);

		if (GUI.Button (new Rect(Screen.width/2 - ((344 * Screen.width * 0.01f * 0.05f)/2), Screen.height * .6f, 344 * Screen.width * 0.01f * 0.05f, 85 * Screen.width * 0.01f * 0.05f), "", playButton)) {
			Application.LoadLevel ("Garden");
		}

		if (GUI.Button (new Rect(Screen.width/2 - ((344 * Screen.width * 0.01f * 0.05f)/2), Screen.height * .6f + (85 * Screen.width * 0.01f * 0.05f) * 1.5f, 344 * Screen.width * 0.01f * 0.05f, 85 * Screen.width * 0.01f * 0.05f), "", optionsButton)) {
			print ("OPCJE");
		}

		if (GUI.Button (new Rect(Screen.width/2 - ((344 * Screen.width * 0.01f * 0.05f)/2), Screen.height * .6f + (85 * Screen.width * 0.01f * 0.05f) * 3f, 344 * Screen.width * 0.01f * 0.05f, 85 * Screen.width * 0.01f * 0.05f), "", exitButton)) {
			print ("WYJSCIE");
		}

	}


}
