using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSet : MonoBehaviour {
	public int roomCount = 4;
	public RoomGroup roomGroup;
	private GameObject stagePrefab;
	[HideInInspector]
	public GameObject[] stages;

	private void Awake() {
		stagePrefab = Resources.Load("Prefabs/Ground") as GameObject;
	}

	void Start () {
		roomGroup = new RoomGroup(roomCount);
		roomGroup.Set();
		DrawMapGroup();
	}
	
	void Update () {
		
	}

	//刷新地图
	void Refresh(){

	}

	//绘制地图群
	public void DrawMapGroup(){
		int xStep = 20, yStep = 15;
		stages = new GameObject[roomCount];
		Vector2[] roomsPos = roomGroup.roomPoses;
		Vector2 startPos = new Vector2(0, 1.11f);
		for(int i = 0; i < roomsPos.Length; i ++){
			stages[i] = GameObject.Instantiate(stagePrefab);
			//设置各个房间的门连接信息
			stages[i].transform.GetChild(0).GetComponent<DoorSet>().connectInfo = roomGroup.rooms[i].surroundInfo;
			stages[i].name = "Ground" + i;
			stages[i].transform.position = startPos + new Vector2(roomsPos[i].x * xStep, roomsPos[i].y * yStep);
		}
	}
}
