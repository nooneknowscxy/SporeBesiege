using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPointShow : MonoBehaviour {

	public Texture2D cursorTexture;
	private void OnGUI() {
		Rect  rect = new Rect(Input.mousePosition.x - (cursorTexture.width >> 1), 
			Screen.height - Input.mousePosition.y - (cursorTexture.height >> 1), cursorTexture.width, cursorTexture.height);
     
    	GUI.DrawTexture(rect, cursorTexture, ScaleMode.ScaleAndCrop);
		Cursor.visible = false;
	}
}
