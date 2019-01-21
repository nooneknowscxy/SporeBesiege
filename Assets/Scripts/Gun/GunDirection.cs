using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDirection : MonoBehaviour {

	Vector2 mousePos2World, shootDir;
	bool faceLeft = true;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		mousePos2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if((mousePos2World.x > transform.position.x && faceLeft) || 
		   (mousePos2World.x < transform.position.x && !faceLeft)){
			faceLeft = !faceLeft;
			Vector3 thisScale = transform.localScale;
			thisScale.x *= -1;
			thisScale.y *= -1;
			transform.localScale = thisScale;
		}
		shootDir = (mousePos2World - (Vector2)transform.position).normalized;
		float angle = Vector2.Angle(shootDir, new Vector2(-1, 0)) * (shootDir.y < 0 ? 1 : -1);
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
