using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public enum BossName
{
	GuardIdiot
}

public class BossManager : MonoBehaviour {

	public BossName bossName;
	///<summary>Boss阶段数</summary>
	private int maxStages;
	///<summary>当前Boss所处阶段</summary>
	private int currentStage = 0;	
	public int maxHp;
	public int[] stageConditionHp;
	private int currentHp;
	public static BossManager Instance{get;private set;}
	
	private void Awake() {
		Instance = this;
	}

	private void Start() {
		maxStages = stageConditionHp.Length;
		currentHp = maxHp;
		switch (bossName)
		{
			case BossName.GuardIdiot:
				break;
			default:break;
		}
	}

	private void Update() {
		switch (bossName)
		{
			//“守卫者”
			case BossName.GuardIdiot:
				break;
			default:break;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.transform.CompareTag("Bullet")){
			//减去伤害值
			currentHp -= GunManager.Instance.hitValue;
			CheckStage();
		}
	}

	///<summary>
	///检查当前状态
	///</summary>
	public void CheckStage(){
		if (currentHp <= stageConditionHp[currentStage])
		{
			currentStage ++;
		}
	}

	///<summary>
	///死亡的延迟
	///</summary>
	public void Dead(float delay){

	}
	
}
