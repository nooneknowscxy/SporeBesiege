using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerRush : MonoBehaviour {

	public TestPlayerAnimationController testPlayerAnimController;
	public  PlayerMove playerMove;
	public PlayerFootArrow arrow;
	public float rushTime = 0.8f;
	
	void Update() {
		Rush();
	}

	void Rush(){
		if(Input.GetKeyDown(KeyCode.E) && testPlayerAnimController.currentStates != PlayerStates.RushDone){
			testPlayerAnimController.currentStates = PlayerStates.Rushing;
			StartCoroutine("Rushing");
		}
	}

	IEnumerator Rushing(){
		yield return null;
		//关闭键盘监听
		playerMove.canListenInput = false;
		playerMove.xSpeed = 20.0f * Mathf.Cos(arrow.angle * Mathf.PI / 180.0f);
		playerMove.ySpeed = 20.0f * Mathf.Sin(arrow.angle * Mathf.PI / 180.0f);
		//冲刺总时间
		yield return new WaitForSeconds(rushTime);
		playerMove.canListenInput = true;
		yield return null;
		testPlayerAnimController.currentStates = PlayerStates.RushDone;
	}
}
