using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FistStatus
{
	///<summary>等待攻击</summary>
	Wait,
	///<summary>攻击准备</summary>
	AttackReady,
	///<summary>攻击中</summary>
	Attack,
	///<summary>攻击完毕</summary>
	AttackDone,
	///<summary>攻击回收</summary>
	Recycle
}
public class FistsController : MonoBehaviour {

	private GameManager gameManager;
	private FistStatus currentStatus;
	private Vector2 playerPos;
	///<summary>使用哪一个拳头</summary>
	private int fistIndex = 0;
	public GameObject[] fists;
	public Collider2D[] fistsCollider;
	
	
	#region 拳头曲线属性
	///<summary>锤子拳头运动曲线</summary>
	public AnimationCurve fistCurve;
	private float time = 0.0f;
	private float moveTime = 0.0f, x, y, fistPosStartX, fistPosStartY;
	private float fistStartPosY = 0.476f;
	private int curveLength;
	///<summary>拳头动画关键帧</summary>
	private Keyframe[] curveFrames;
	#endregion
	
	
	///<summary>预警圈</summary>
	public GameObject waringCircle;
	public float waitTime, attackReadyTime, attackTime, attackDoneTime, RecycleTime;
	private bool isConfirm = false;
	
	private void Start() {
		curveFrames = fistCurve.keys;
		curveLength = fistCurve.length;

		gameManager = GameManager.Instance;	
		StartCoroutine("SwitchStatus");	
	}

	private void Update() {
		switch (currentStatus)
		{
			case FistStatus.Wait:
				waringCircle.SetActive(false);
				break;
			case FistStatus.AttackReady:
				waringCircle.SetActive(true);
				fistsCollider[fistIndex].enabled = true;
				StartCoroutine("AttackReady");
				break;
			case FistStatus.Attack:
				waringCircle.SetActive(false);
				fistsCollider[fistIndex].enabled = false;
				MoveFist(playerPos);
				GetFistCurveValue();
				break;
			case FistStatus.AttackDone:
				fistsCollider[fistIndex].enabled = true;
				time = 0.0f;
				break;
			default:break;
		}
	}

	IEnumerator AttackReady(){
		//确定主角位置
		if (!isConfirm)
		{
			//预警圈移动到角色脚底下
			playerPos = gameManager.playerTransform.GetChild(3).position;
			//记录拳头出动前起始位置
			fistPosStartX = fists[fistIndex].transform.position.x;
			fistPosStartY = fists[fistIndex].transform.position.y;
			yield return null;
			waringCircle.transform.position = playerPos;
			isConfirm = true;
		}
	}

	///<summary>
	///拳头插值移动到预警圈位置
	///</summary>
	void MoveFist(Vector2 target){
		float step = Time.deltaTime / attackTime;
		x = Mathf.Lerp(fistPosStartX, target.x, moveTime);
		y = Mathf.Lerp(fistPosStartY, target.y, moveTime);
		moveTime += step;
		fists[fistIndex].transform.position = new Vector2(x, y);
	}

	///<summary>
	///拳头运动曲线
	///</summary>
	void GetFistCurveValue(){
		if(time < curveFrames[curveLength - 1].time){
			time += Time.deltaTime;
			float value = fistCurve.Evaluate(time);
			//Debug.Log("Time = " + time + " Curve Value = " + value);

			float x = fists[fistIndex].transform.GetChild(0).localPosition.x;
			float y = fistStartPosY + value;
			fists[fistIndex].transform.GetChild(0).localPosition = new Vector2(x, y);
		}
	}

	void ResetFist(){
		isConfirm = false;
		moveTime = 0.0f;
	}

	///<summary>
	///切换拳头状态
	///</summary>
	IEnumerator SwitchStatus(){
		while(this.enabled == true){
			currentStatus = FistStatus.Wait;
			yield return new WaitForSeconds(waitTime);
			currentStatus = FistStatus.AttackReady;
			yield return new WaitForSeconds(attackReadyTime);
			currentStatus = FistStatus.Attack;
			yield return new WaitForSeconds(attackTime);
			currentStatus = FistStatus.AttackDone;
			yield return new WaitForSeconds(attackDoneTime);
			currentStatus = FistStatus.Recycle;
			yield return new WaitForSeconds(RecycleTime);
			ResetFist();
		}
	}

	private void OnEnable() {
		StartCoroutine("SwitchStatus");	
	}

}
