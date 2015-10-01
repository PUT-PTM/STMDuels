using UnityEngine;
using System.Collections;

public class customCursor : MonoBehaviour {

	public Texture cursor;

	private int drawDepth = -1000;
	private int h = 32;
	private int w = 32;
	private Vector2 mouse;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		mouse = new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}

	void OnGUI() {
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (mouse.x - (w / 2), mouse.y - (h / 2), w, h), cursor);
	}
}
