using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
	Idle,
	Walking,
	Rushing,
	RushDone
}

public class TestPlayerAnimationController : MonoBehaviour {

	public PlayerStates currentStates;
	public Animator rushFireAnimator;
	private Rigidbody2D playerRigidbody;
	private Animator playerAnimator;
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
	}
	
	void Update () {
		//if(!(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)){
		if(currentStates != PlayerStates.Rushing){
			if(playerRigidbody.velocity.sqrMagnitude != 0){
				currentStates = PlayerStates.Walking;
			}else
			{
				currentStates = PlayerStates.Idle;
			}
		}
		

		switch (currentStates)
		{
			case PlayerStates.Idle:
				playerAnimator.SetBool("Rushing", false);
				rushFireAnimator.SetBool("Rushing", false);
				playerAnimator.SetBool("Walking", false);
				playerAnimator.SetBool("Rushing", false);
				break;
			case PlayerStates.Walking:
				playerAnimator.SetBool("Walking", true);
				playerAnimator.SetBool("Rushing", false);
				break;
			case PlayerStates.Rushing:
				playerAnimator.SetBool("Rushing", true);
				rushFireAnimator.SetBool("Rushing", true);
				break;
			default:break;
		}

	}
}
