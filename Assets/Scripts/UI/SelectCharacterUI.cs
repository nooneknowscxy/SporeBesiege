using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterUI : MonoBehaviour {

	public GameObject test;
	public Transform wannaParent;

	public void AddItem(){
		GameObject.Instantiate(test, wannaParent);
	}
}
