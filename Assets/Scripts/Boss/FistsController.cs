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
	Recycle,
	///<summary>攻击回收</summary>
	RecycleDone
}
public class FistsController : MonoBehaviour {

	private GameManager gameManager;
	private FistStatus currentStatus;
	private Vector3 playerPos;
	///<summary>使用哪一个拳头</summary>
	private int fistIndex = 0;
	public GameObject[] fists;
	public Collider2D[] fistsCollider;
	
	
	#region 拳头曲线属性
	///<summary>锤子拳头运动曲线</summary>
	public AnimationCurve fistCurve;
	private float time = 0.0f;
	private float moveTime = 0.0f, x, y, z, fistPosX, fistPosY;
	///<summary>最起始拳头位置</summary>
	private float fistStartPosX, fistStartPosY, fistStartPosZ, fistStartY = 0.476f;
	private int curveLength;
	///<summary>拳头动画关键帧</summary>
	private Keyframe[] curveFrames;
	#endregion
	
	
	///<summary>预警圈</summary>
	public GameObject waringCircle;
	public float waitTime, attackReadyTime, attackTime, attackDoneTime, recycleTime;
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

				//记录当前所用拳头的起始位置
				fistStartPosX = fists[fistIndex].transform.position.x;
				fistStartPosY = fists[fistIndex].transform.position.y;
				fistStartPosZ = fists[fistIndex].transform.position.z;

				break;
			case FistStatus.AttackReady:
				waringCircle.SetActive(true);
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
				moveTime = 0.0f;
				break;
			case FistStatus.Recycle:
				RecycleFist();
				break;
			case FistStatus.RecycleDone:
				//切换拳头
				StartCoroutine("SwitchFist");
				break;
			default:break;
		}
	}

	IEnumerator AttackReady(){
		//确定主角位置
		if (!isConfirm)
		{
			//预警圈移动到角色脚底下
			playerPos = gameManager.playerTransform.GetChild(4).position;
			//记录拳头出动前起始位置
			fistPosX = fists[fistIndex].transform.position.x;
			fistPosY = fists[fistIndex].transform.position.y;
			yield return null;
			waringCircle.transform.position = playerPos;
			fistsCollider[fistIndex].enabled = true;
			isConfirm = true;
		}
	}

	///<summary>
	///拳头插值移动到预警圈位置
	///</summary>
	void MoveFist(Vector3 target){
		float step = Time.deltaTime / attackTime;
		x = Mathf.Lerp(fistPosX, target.x, moveTime);
		y = Mathf.Lerp(fistPosY, target.y, moveTime);
		z = target.z;
		moveTime += step;
		fists[fistIndex].transform.position = new Vector3(x, y, z);
	}

	///<summary>
	///拳头回到原来的位置
	///</summary>
	void RecycleFist(){
		float step = Time.deltaTime / recycleTime;
		x = Mathf.Lerp(playerPos.x, fistStartPosX, moveTime);
		y = Mathf.Lerp(playerPos.y, fistStartPosY, moveTime);
		z = fistStartPosZ;
		moveTime += step;
		fists[fistIndex].transform.position = new Vector3(x, y, z);
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
			float y = fistStartY + value;
			fists[fistIndex].transform.GetChild(0).localPosition = new Vector2(x, y);
		}
	}

	///<summary>
	///重置拳头通用属性
	///</summary>
	void ResetFist(){
		isConfirm = false;
		moveTime = 0.0f;
	}

	///<summary>
	///切换使用的拳头
	///</summary>
	IEnumerator SwitchFist(){
		yield return null;
		fistIndex = fistIndex < 1 ? 1 : 0; 
		Debug.Log(fistIndex);
		//fistIndex = 1;
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
			yield return new WaitForSeconds(recycleTime);
			currentStatus = FistStatus.RecycleDone;
			ResetFist();
			yield return null;
		}
	}

	private void OnEnable() {
		StartCoroutine("SwitchStatus");	
	}

}
