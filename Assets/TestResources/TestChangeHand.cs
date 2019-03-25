using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeHand : MonoBehaviour {

	public SpriteRenderer gun;
	private Sprite oldGun;
	public Sprite newGun;
	public SpriteRenderer hand;
	public GunDirection direction;
	void Start () {
		hand.enabled = false;
		oldGun = gun.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(direction.angle > 15.0f && direction.angle < 165.0f){
			hand.enabled = true;
			gun.sprite = newGun;
		}else
		{
			hand.enabled = false;
			gun.sprite = oldGun;
		}
	}
}
