using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerAnimationController : MonoBehaviour {

	private Rigidbody2D playerRigidbody;
	private Animator playerAnimator;
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
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
