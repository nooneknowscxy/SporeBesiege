using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBoss : BossManager{
	[Header("Unique Property")]
	public FistsController fistController;

	void Start() {
		Init();
		bossName = BossName.GuardIdiot;
	}

	private void Update() {
		switch (currentStage)
		{
			case 0:
				Debug.Log("Guard at stage 1!");
				break;
			case 1:
				Debug.Log("Guard at stage 2!");
				break;
			case 2:
				Debug.Log("Guard at stage 3!");
				break;
			case 3:
				Debug.Log("Guard at stage 4!");
				break;
			default:break;
		}
	}
}
