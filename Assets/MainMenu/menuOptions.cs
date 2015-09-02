using UnityEngine;
using System.Collections;

public class menuOptions : MonoBehaviour {
	
	public Texture backgroundTexture;
	public Texture logo;
	public GUIStyle goBackButton;
	
	IEnumerator ChangeLevel () {
		float fadeTime = GameObject.Find("Fading").GetComponent<fadingBlack>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(0);
	}
	
	void OnGUI(){
		
		
		
		// Rysowanie tła
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		
		// Rysowanie loga
		GUI.DrawTexture (new Rect (Screen.width/2 - (596 * Screen.width * 0.01f * 0.05f)/2, Screen.height * 0.08f, 596 * Screen.width * 0.01f * 0.05f, 177 * Screen.width * 0.01f * 0.05f), logo);

		if (GUI.Button (new Rect(Screen.width * 0.05f, Screen.height * 0.14f, 98 * Screen.width * 0.01f * 0.05f, 63 * Screen.width * 0.01f * 0.05f), "", goBackButton)) {
			StartCoroutine(ChangeLevel());
		}
	}
	
	
	
	
}
