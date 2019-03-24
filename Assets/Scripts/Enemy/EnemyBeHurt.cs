using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeHurt : MonoBehaviour {

	public Animator hurtAnimator;
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.transform.CompareTag("Bullet")){
			hurtAnimator.SetTrigger("BeHurt");
		}
	}
}
