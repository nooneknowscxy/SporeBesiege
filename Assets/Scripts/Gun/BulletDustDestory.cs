using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDustDestory : MonoBehaviour {

	private ParticleSystem bulletDust;
	private float duration;
	void Start () {
		bulletDust = transform.GetComponent<ParticleSystem>();
		duration = bulletDust.main.duration;
		StartCoroutine(DestorySelf());
	}
	
	IEnumerator DestorySelf(){
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

}
