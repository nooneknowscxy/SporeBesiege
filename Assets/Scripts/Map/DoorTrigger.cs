using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	private RoomPointer pointer;
	private GameManager gameManager;
	private int doorDir;
	void Start () {
		string doorName = gameObject.name.Substring(4, 1);
		doorDir = int.Parse(doorName);
		pointer = RoomPointer.Instance;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals("Player")){
			pointer.Move(doorDir);
			Debug.Log("Enter Direction = " + doorDir);

			//移动镜头
			CameraMove.Instance.MoveByDir(doorDir);

			PlayerPrefs.SetInt("EnterDoorSet", 1);
			PlayerPrefs.SetInt("EnterDoorNum", doorDir);
			gameManager.canSwitchScene = true;
		}
	}
}
