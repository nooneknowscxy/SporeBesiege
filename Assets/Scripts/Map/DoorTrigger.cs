using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorTrigger : MonoBehaviour {

	private bool canSwitch = true;
	public float targetTime = 2.0f;
	private bool isNearDoor = false;
	public float stayTime = 0f;
	private RoomPointer pointer;
	private GameManager gameManager;
	private int doorDir;
	void Start () {
		string doorName = gameObject.name.Substring(4, 1);
		doorDir = int.Parse(doorName);
		pointer = RoomPointer.Instance;
		gameManager = GameManager.Instance;
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		/*if(other.CompareTag("Player")){
			pointer.Move(doorDir);
			Debug.Log("Enter Direction = " + doorDir);

			//移动镜头
			CameraMove.Instance.MoveByDir(doorDir);

			PlayerPrefs.SetInt("EnterDoorSet", 1);
			PlayerPrefs.SetInt("EnterDoorNum", doorDir);
			gameManager.canSwitchScene = true;
		}*/
	}

	private void Update() {
		if(isNearDoor){
			stayTime += Time.deltaTime;
		}

		//设置门的状态切换
		if(stayTime >= targetTime && canSwitch){
			//重置待着的时间
			stayTime = 0.0f;
			pointer.Move(doorDir);
			Debug.Log("Enter Direction = " + doorDir);
			//移动镜头
			CameraMove.Instance.MoveByDir(doorDir);

			//标记当前进入的门
			this.tag = "ActiveDoor";

			PlayerPrefs.SetInt("EnterDoorSet", 1);
			PlayerPrefs.SetInt("EnterDoorNum", doorDir);
			gameManager.status = GameStatus.SwitchScene;
			canSwitch = false;
		}
	}
	private void OnTriggerStay2D(Collider2D other) {
		isNearDoor = other.CompareTag("Player") ? true : false;
	}

	private void OnTriggerExit2D(Collider2D other) {
		isNearDoor = false;
		stayTime = 0.0f;
	}
}
