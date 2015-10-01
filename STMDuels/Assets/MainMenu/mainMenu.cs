using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {
	
	public Texture backgroundTexture;
	public Texture logo;
	public GUIStyle playButton;
	public GUIStyle optionsButton;
	public GUIStyle exitButton;

	IEnumerator ChangeLevel () {
		float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(1);
	}

	IEnumerator LoadLevel () {
		float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(3);
	}

	IEnumerator ExitGame() {
		float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.Quit();
	}

	void OnGUI(){



		// Rysowanie tła
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		// Rysowanie loga
		GUI.DrawTexture (new Rect (Screen.width/2 - (700 * Screen.width * 0.01f * 0.05f)/2, Screen.height * 0.05f, 700 * Screen.width * 0.01f * 0.05f, 400 * Screen.width * 0.01f * 0.05f), logo);

		// Rysowanie przycisków
		if (GUI.Button (new Rect(Screen.width/2 - ((344 * Screen.width * 0.01f * 0.05f)/2), Screen.height * .62f, 344 * Screen.width * 0.01f * 0.05f, 85 * Screen.width * 0.01f * 0.05f), "", playButton)) {
			StartCoroutine(LoadLevel());
		}
		
		if (GUI.Button (new Rect(Screen.width/2 - ((344 * Screen.width * 0.01f * 0.05f)/2), Screen.height * .62f + (85 * Screen.width * 0.01f * 0.05f) * 1.5f, 344 * Screen.width * 0.01f * 0.05f, 85 * Screen.width * 0.01f * 0.05f), "", optionsButton)) {
			StartCoroutine(ChangeLevel());
			//print ("OPCJE");
		}
		
		if (GUI.Button (new Rect(Screen.width/2 - ((344 * Screen.width * 0.01f * 0.05f)/2), Screen.height * .62f + (85 * Screen.width * 0.01f * 0.05f) * 3f, 344 * Screen.width * 0.01f * 0.05f, 85 * Screen.width * 0.01f * 0.05f), "", exitButton)) {
			StartCoroutine(ExitGame());
		}
		
	}


	
	
}
