using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

	public bool isMan = true;
	public AnimationClip[] genderAnimation;
	private Rigidbody2D playerRigidbody;
	private Animator playerAnimator;
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();


		//根据性别更改动画片段
		AnimatorOverrideController overrideController = new AnimatorOverrideController();
		overrideController.runtimeAnimatorController  = playerAnimator.runtimeAnimatorController;
		if (isMan)
		{
			overrideController["woman_walk"]  = genderAnimation[0];	
			overrideController["woman_idle"]  = genderAnimation[2];	
		}else
		{
			overrideController["man_walk"]  = genderAnimation[1];	
			overrideController["man_idle"]  = genderAnimation[3];	
		}
		playerAnimator.runtimeAnimatorController = overrideController;
	}
	
	void Update () {
		//if(!(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)){
		if(playerRigidbody.velocity.sqrMagnitude != 0){
			playerAnimator.SetBool("Walking", true);
		}else
		{
			playerAnimator.SetBool("Walking", false);
		}

	}
}
