using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Transform playerTransform;
	private PlayerMove playerCtrl;
	private DoorSet doorSet;
	public bool canSwitchScene = false, canPlayerMove = true;
	void Start () {
		playerTransform = GameObject.Find("Player").transform;
		playerCtrl = playerTransform.GetComponent<PlayerMove>();
	}
	
	void Update () {
		if (canSwitchScene)
		{
			int previousDoor = PlayerPrefs.GetInt("EnterDoorNum");
			PlayerPrefs.SetInt("EnterDoorNum", -1);
			int currentDoor = OppositeDoor(previousDoor);

			//设置角色位置
			GetDoorPos();
			int currentRoom = RoomPointer.Instance.GetNumber();
			MapSet roomMap = GameObject.Find("MapBuilder").GetComponent<MapSet>();
			DoorSet doors = roomMap.stages[currentRoom].GetComponentInChildren<DoorSet>();
			//获得入门位置
			Vector2 targetPos = doors.enterPoses[currentDoor].position;
			playerTransform.position = targetPos;
			//设置角色停止
			playerCtrl.Stop();
			//关闭按键监听
			playerCtrl.enabled = false;
			canPlayerMove = false;
			StartCoroutine(WaitForSwitchDone());
			canSwitchScene = false;	
		}

		Debug.Log("Current Room :" + RoomPointer.Instance.GetNumber());
	}

	IEnumerator WaitForSwitchDone(){
		while(true){
			yield return null;
			if(canPlayerMove){
				//开启按键监听
				playerCtrl.enabled = true;
				break;
			}
			//playerCtrl.Stop();
		}
	}

	void GetDoorPos(){
		doorSet = GameObject.Find("Doors").GetComponent<DoorSet>();
	}

	void LoadNewRoom(){
		SceneManager.UnloadSceneAsync(1);
		SceneManager.LoadScene(1, LoadSceneMode.Additive);
	}

	protected int OppositeDoor(int num){
		if(num >= 2){
			return ((num + 2) % 4);
		}else
		{
			return num + 2;
		}
	}
}
