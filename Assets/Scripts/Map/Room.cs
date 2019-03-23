using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{
	public int number;	//编号
	public Vector2 pos;	//初始默认位置
	public int[] surroundInfo;	//周边房间信息
	public Room(){
		number = 0;
		surroundInfo = new int[4]{-1, -1, -1, -1};
		pos = new Vector2(0, 0);
	}
	public Room(int n){
		number = n;
		surroundInfo = new int[4]{-1, -1, -1, -1};
		pos = new Vector2(0, 0);
	}

	public Room(int n, Vector2 position){
		number = n;
		surroundInfo = new int[4]{-1, -1, -1, -1};
		pos = position;
	}

	public bool hasEmptySpace(){
		for(int i = 0; i < 4; i ++){
			if(this.surroundInfo[i] == -1){
				return true;
			}
		}
		return false;
	}

	public bool isFullInDir(int dir){
		List<int> index = new List<int>();
		for(int i = 0; i < 4; i ++){
			if(this.surroundInfo[i] != -1){
				index.Add(i);
			}
		}
		foreach(int n in index){
			if(n == dir){
				return true;
			}
		}
		return false;
	}
}

public class RoomGroup{
	int roomCount;	//房间数量
	public Room[] rooms;
	public Vector2[] roomPoses;
	public RoomGroup(int count = 4){
		roomCount = count;
		rooms = new Room[count];
		roomPoses = new Vector2[count];
	}
	void Init(){
		for(int i = 0; i < roomCount; i ++){
			rooms[i] = new Room(i);
		}
	}

	public void Set(){
		Init();

		//主干道房间数量
		int mainCount = (int)(roomCount * 2 / 3);
		//主干道的生成
		for(int i = 0; i < mainCount; i ++){
			if(i < mainCount - 1){
				MoveRoomTo(rooms[i], i + 1);
			}
			//更新房间的位置
			roomPoses[i] = rooms[i].pos;
		}

		//支路的生成
		int branchStart = (int)Random.Range(1, mainCount - 1);
		//Debug.Log("BranchStart :" + branchStart);
		while(!rooms[branchStart].hasEmptySpace()){
			branchStart = (int)Random.Range(1, mainCount);
		}
		for(int i = mainCount; i < roomCount; i ++){
			if(i == mainCount){
				MoveRoomTo(rooms[branchStart], i);
			}
			if(i < roomCount - 1)
			{
				MoveRoomTo(rooms[i], i + 1);
			}
			roomPoses[i] = rooms[i].pos;
		}

		//Log输出
		for(int i = 0; i < roomCount; i ++){
			//Debug.Log("Room" + i + " pos = " + rooms[i].pos);
			//Debug.Log("Room" + i + " Up:" + rooms[i].surroundInfo[0] + " Right:" + rooms[i].surroundInfo[1] + " Down:" + rooms[i].surroundInfo[2] + " Left:" + rooms[i].surroundInfo[3]);
			//Debug.Log("Room " + i + ": " + roomPoses[i]);
		}
	}

	
	//将房间room与target房间号的房间连接
	void MoveRoomTo(Room room, int target){
		int randomDir = GetRandomDir();
		//如果该方向没有房间
		//if(room.surroundInfo[randomDir] == -1 && !isExistRoom(room, randomDir)){
		if(room.surroundInfo[randomDir] == -1 && !room.isFullInDir(randomDir)){
			room.surroundInfo[randomDir] = target;
			//设置房间位置
			GetRoomByNum(target).pos = ChangePos(room, randomDir);
			GetRoomByNum(target).surroundInfo[OppositeDir(randomDir)] = room.number;
		}else{
			int newDir = randomDir;
			while(newDir == randomDir || room.isFullInDir(newDir)){
				newDir = GetRandomDir();
			}
			room.surroundInfo[newDir] = target;
			GetRoomByNum(target).pos = ChangePos(room, newDir);
			GetRoomByNum(target).surroundInfo[OppositeDir(newDir)] = room.number;
		}
	}

	Vector2 ChangePos(Room room, int dir){
		Vector2 pos = room.pos;
		switch(dir){
			case 0:	//上\
				pos = room.pos + new Vector2(0, 1);break;
			case 1:	//右
				pos = room.pos + new Vector2(1, 0);break;
			case 2:	//下
				pos = room.pos + new Vector2(0, -1);break;
			case 3:	//左
				pos = room.pos + new Vector2(-1, 0);break;
			default:break;
		}
		return pos;
	}

	///<summary>
	///获取出/入口对向的方向
	///</summary>
	protected int OppositeDir(int num){
		if(num >= 2){
			return ((num + 2) % 4);
		}else
		{
			return num + 2;
		}
	}

	///<summary>
	///根据房间编号获取房间
	///</summary>
	Room GetRoomByNum(int target){
		Room targetRoom = new Room();
		foreach(Room room in rooms){
			if(room.number == target){
				targetRoom = room;
			}
		}
		return targetRoom;
	}

	///<summary>
	///根据房间位置获取房间
	///</summary>
	public Room GetRoomByPos(Vector2 pos){
		Room targetRoom = new Room();
		for(int i = 0; i < roomPoses.Length; i ++){
			if(roomPoses[i].Equals(pos)){
				targetRoom = rooms[i];
				return targetRoom;
			}
		}
		return targetRoom;
	}

	//随机一个方向
	int GetRandomDir(){
		return (int)Random.Range(0, 4);
	}

}
