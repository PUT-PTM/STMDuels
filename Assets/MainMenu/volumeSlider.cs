using UnityEngine;
using System.Collections;

public class volumeSlider : MonoBehaviour {

	public GameObject audioVolume;

	private float hSliderValue = 1.0f;

	// Use this for initialization
	
	void OnGUI () {
		hSliderValue = GUI.HorizontalSlider(new Rect(0.3f*Screen.width, 0.4f*Screen.height, 0.4f*Screen.width, 25), hSliderValue, 0.0f, 1.0f);
	}
}
