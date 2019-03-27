using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	private Rigidbody2D player;
	public Vector2 maxSpeed = new Vector2(3.0f, 3.0f);
	public float minSpeed = 0.08f;
	public float acceleration = 10.0f;
	public float reactConst = 0.5f;
	public float friction = 2.0f;
	[HideInInspector]
	public float xSpeed, ySpeed;
	///<summary>键盘监听开关</summary>
	[HideInInspector]
	public bool canListenInput = true;

	public bool isFaceLeft = true;

	
	void Start () {
		player = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		SmoothMove();
		MoveFlip();
	}

	void SmoothMove(){
		float xInput, yInput;
		xInput = canListenInput ? Input.GetAxisRaw("Horizontal") : 0;
		yInput = canListenInput ? Input.GetAxisRaw("Vertical") : 0;	

		//如果按键方向与移动方向相反
		if(xInput * player.velocity.x < 0){
			xSpeed = xSpeed + xInput * acceleration * reactConst * Time.deltaTime;
		}
		if(yInput * player.velocity.y < 0){
			ySpeed = ySpeed + yInput * acceleration * reactConst * Time.deltaTime;
		}
		
		//如果相同
		if(xInput * player.velocity.x >= 0)
		{
			xSpeed = xSpeed + xInput * acceleration * Time.deltaTime;
		}
		if(yInput * player.velocity.y >= 0)
		{
			ySpeed = ySpeed + yInput * acceleration * Time.deltaTime;
		}	
		
		if(xInput == 0 && Mathf.Abs(player.velocity.x) > 0){
			xSpeed = xSpeed - Mathf.Sign(player.velocity.x) * friction * Time.deltaTime;
		}
		if(yInput == 0 && Mathf.Abs(player.velocity.y) > 0){
			ySpeed = ySpeed - Mathf.Sign(player.velocity.y) * friction * Time.deltaTime;
		}

		//防止在不操作时剧烈鬼畜移动
		if(Mathf.Abs(xSpeed) <= minSpeed){
			xSpeed = 0;
		}
		if(Mathf.Abs(ySpeed) <= minSpeed){
			ySpeed = 0;
		}

		xSpeed = Mathf.Clamp(xSpeed, -maxSpeed.x, maxSpeed.x);
		ySpeed = Mathf.Clamp(ySpeed, -maxSpeed.y, maxSpeed.y);

		player.velocity = new Vector2(xSpeed, ySpeed);
	}

	void MoveFlip(){
		//检测鼠标与角色的关系
		//将鼠标坐标转换为世界坐标
		Vector2 mousePos2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if((mousePos2World.x > transform.position.x && isFaceLeft) || 
		   (mousePos2World.x < transform.position.x && !isFaceLeft)){
			isFaceLeft = !isFaceLeft;
			Vector3 thisScale = transform.localScale;
			thisScale.x *= -1;
			transform.localScale = thisScale;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Barrier"){
			Stop();
		}
	}

	public void Stop(){
		//重置输入值
		xSpeed = 0;
		ySpeed = 0;
		//角色停止
		player.velocity = Vector2.zero;
	}
}
