using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentBullet : MonoBehaviour {

	private Text text;
	void Start () {
		text = transform.GetComponent<Text>();
	}
	
	void Update () {
		text.text = GameObject.Find("Muzzle").GetComponent<GunManager>().BulletNumber.ToString();
	}
}
