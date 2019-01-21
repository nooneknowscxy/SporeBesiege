using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootArrow : MonoBehaviour {

	private Transform arrow;
	Vector2 temp;
	void Start () {
		arrow = GetComponent<Transform>();
		temp = new Vector2(-1, 0);
	}
	
	void Update () {
		Vector2 arrowVect = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if(arrowVect != Vector2.zero){
			temp = arrowVect;
		}
		float angle = Vector2.Angle(temp, new Vector2(1, 0)) * (temp.y > 0 ? 1 : -1);
		arrow.rotation = Quaternion.Euler(0, 0, angle);
	}
}
