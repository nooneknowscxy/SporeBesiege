using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour {

	public static PropManager Instance{get;private set;}
	private void Awake() {
		Instance = this;
	}
	void FindProp(int id){
		
	}
}
