using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public enum BossName
{
	GuardIdiot
}

public class BossManager : MonoBehaviour {

	protected BossName bossName;
	///<summary>Boss阶段数</summary>
	private int maxStages;
	///<summary>当前Boss所处阶段</summary>
	protected int currentStage = 0;	
	public int maxHp;
	public int[] stageConditionHp;
	private int currentHp;
	public static BossManager Instance{get;private set;}
	
	private void Awake() {
		Instance = this;
	}

	void Start() {
		Init();
	}
	
	///<summary>
	///初始化最大阶段数与当前生命值
	///</summary>
	protected void Init(){
		maxStages = stageConditionHp.Length;
		currentHp = maxHp;
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
			if (currentHp == 0)
			{
				//执行死刑，可以挂接死亡动画
				Dead(0.0f);
			}else
			{
				currentStage ++;	
			}
		}
	}

	///<summary>
	///死亡的延迟
	///</summary>
	public void Dead(float delay){

	}
	
}
