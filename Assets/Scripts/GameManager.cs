using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameStatus
{
	Playing,
	Pause,
	SwitchScene,
	SwitchNotDetect,
	SwitchDone,
	WantExit,
	Exit
}
public class GameManager : MonoBehaviour {

	public GameStatus status;
	private Transform playerTransform;
	private PlayerMove playerCtrl;
	private DoorSet doorSet;
	public static GameManager Instance{get; private set;} 
	private void Awake() {
		Instance = this;
	}
	void Start () {
		playerTransform = GameObject.Find("Player").transform;
		playerCtrl = playerTransform.GetComponent<PlayerMove>();
	}
	
	void Update () {
		Debug.Log("GameStatus = " + status);
		switch(status){
			case GameStatus.Playing:
				Time.timeScale = 1.0f;
				break;
			case GameStatus.Pause:
				Time.timeScale = 0.0f;
				break;
			case GameStatus.SwitchScene:
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
				StartCoroutine(WaitForSwitchDone());
				//脱离状态
				status = GameStatus.SwitchNotDetect;
				break;
			case GameStatus.SwitchNotDetect:break;
			case GameStatus.SwitchDone:
				GameObject.FindGameObjectWithTag("ActiveDoor").tag = "NoActiveDoor";
				break;
			default:break;
		}

		//Debug.Log("Current Room :" + RoomPointer.Instance.GetNumber());
	}

	IEnumerator WaitForSwitchDone(){
		while(true){
			yield return null;
			if(status == GameStatus.SwitchDone){
				//开启按键监听
				playerCtrl.enabled = true;
				status = GameStatus.Playing;
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
