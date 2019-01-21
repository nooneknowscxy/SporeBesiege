﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentNum : MonoBehaviour {

	private Text text;
	void Start () {
		text = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = RoomPointer.Instance.GetNumber().ToString();
	}
}
