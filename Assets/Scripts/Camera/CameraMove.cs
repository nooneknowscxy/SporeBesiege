using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public float moveRate = 2.0f;
	private GameManager gameManager;
	private Camera mainCamera;
	private Vector2 targetPos;
	public bool canMove = false;
	float movePercent = 0.0f;

	public static CameraMove Instance{get; private set;}
	void Awake () {
		Instance = this;
	}
	private void Start() {
		mainCamera = GetComponent<Camera>();
		gameManager = GameManager.Instance;
	}
	
	public void MoveByDir (int dir) {
		targetPos = transform.position;
		switch(dir){
			case 0:	//上
				targetPos = new Vector2(0, 15) + (Vector2)transform.position; break;
			case 1:	//右
				targetPos = new Vector2(20, 0) + (Vector2)transform.position; break;
			case 2:	//下
				targetPos = new Vector2(0, -15) + (Vector2)transform.position; break;
			case 3:	//左
				targetPos = new Vector2(-20, 0) + (Vector2)transform.position; break;
			default:break;
		}

		canMove = true;
	}

	private void Update() {
		if(canMove){
			Vector2 temp = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * moveRate);
			transform.position = new Vector3(temp.x, temp.y, transform.position.z);
			movePercent += Time.deltaTime;
			//Debug.Log("movePercent = " + movePercent);
			if(movePercent > 1.0f){
				canMove = false;
				movePercent = 0.0f;
				
				//传递已经摄像头到位的信息
				gameManager.status = GameStatus.SwitchDone;
			}
		}
	}
}
