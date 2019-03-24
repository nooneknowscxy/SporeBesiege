using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	void Start () {
		SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
	}
	
	void Update () {
		
	}
}
