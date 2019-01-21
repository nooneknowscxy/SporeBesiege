using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	private int hitPoint;
	private float moveSpeed;
	private float packageNum;
	public static PlayerData Instance{get; private set;}
	void Awake () {
		Instance = this;
	}
}
