using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSet : MonoBehaviour {
	public GameObject[] doors;
	public Transform[] enterPoses;
	public int[] connectInfo;
	private RoomPointer pointer;
	void Start () {
		pointer = RoomPointer.Instance;
		//初始化场景内门的信息
		//获得connectInfo通过修改代码执行顺序
		SetDoorActive();
	}
	
	void SetDoorActive(){
		for(int i = 0; i < 4; i ++){
			doors[i].SetActive(connectInfo[i] == -1 ? false : true);
			doors[i].GetComponent<DoorTrigger>().enabled = (connectInfo[i] == -1 ? false : true);
			doors[i].GetComponent<BoxCollider2D>().enabled = (connectInfo[i] == -1 ? false : true);
		}
	}
}
