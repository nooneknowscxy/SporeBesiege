using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPointer : MonoBehaviour {

	public RoomGroup roomGroup;
	private Vector2 pointPos;	//虚拟网格内位置
	private Room currentRoom;
	private int roomNum = 0;
	public static RoomPointer Instance{get; private set;}
	
	void Awake() {
		Instance = this;
	}

	private void Start() {
		roomGroup = GameObject.Find("MapBuilder").GetComponent<MapSet>().roomGroup;
		pointPos = new Vector2(0, 0);
	}

	Room GetRoom(){
		Room room = roomGroup.GetRoomByPos(pointPos);
		return room;
	}

	//获取房间号
	public int GetNumber(){
		return GetRoom().number;
	}

	//获取门的连接属性
	public int[] GetDoors(){
		return GetRoom().surroundInfo;
	}

	public Vector2 GetPos(){
		return GetRoom().pos;
	}

	public void Move(int dir){
		switch(dir){
			case 0:	//上\
				pointPos = pointPos + new Vector2(0, 1);break;
			case 1:	//右
				pointPos = pointPos + new Vector2(1, 0);break;
			case 2:	//下
				pointPos = pointPos + new Vector2(0, -1);break;
			case 3:	//左
				pointPos = pointPos + new Vector2(-1, 0);break;
			default:break;
		}
	}
}
