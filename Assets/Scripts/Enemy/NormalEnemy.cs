using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStatus
{
	Patrol,
	WannaAttack,
	AngerAttack,
	BeAttack,
	LoseAim,
	Dead
}

public class NormalEnemy : MonoBehaviour {

	private Rigidbody2D enemyRigidbody;
	public float patrolSpeed = 2.0f, chaseSpeed = 3.0f;
	public float alertDistance = 2.0f;	//警觉距离
	public float releaseAfterTime = 1.0f;
	private float patrolTimer = 0.0f;
	private EnemyStatus currentStatus;
	private GameObject player;
	void Start () {
		enemyRigidbody = GetComponent<Rigidbody2D>();
		player = GameObject.Find("FootPoint");
		currentStatus = EnemyStatus.Patrol;
	}
	

	void Update () {
		switch (currentStatus)
		{
			case EnemyStatus.Patrol:
				//释放所有协程
				try
				{
					StopCoroutine("ReleaseWarning");
					StopCoroutine("ReleaseAnger");
				}
				catch (System.Exception)
				{
					Debug.Log("No Coroutine");
					throw;
				}

				patrolTimer += Time.deltaTime;
				if(patrolTimer >= 1f){
					patrolTimer = 0.0f;
					Patrol();
				}
				//Debug.Log(patrolTimer);
				DetectPlayer();
				break;
			case EnemyStatus.WannaAttack:
				DetectPlayer();
				Move2Player();
				break;
			case EnemyStatus.AngerAttack:
				Move2Player();
				StartCoroutine("ReleaseAnger");
				break;	
			case EnemyStatus.BeAttack:
				//被攻击后进入暴怒攻击状态
				currentStatus = EnemyStatus.AngerAttack;
				break;
			case EnemyStatus.LoseAim:
				Stop();
				StartCoroutine("ReleaseWarning");
				break;	
			default:break;
		}

		//Debug.Log("Status = " + currentStatus);
	}

	//巡逻模式
	void Patrol(){
		int x = (int)Random.Range(-2, 2);
		int y = (int)Random.Range(-2, 2);
		if(Mathf.Abs(x) > 0){
			y = 0;
		}else
		{
			while(y == 0){
				y = (int)Random.Range(-2, 2);
			}
		}
		Vector2 direction = new Vector2(x, y);
		enemyRigidbody.velocity = direction * patrolSpeed;
		Debug.Log("Patrol Direction = " + direction);
	}
	
	//向玩家靠近
	void Move2Player(){
		Vector2 direction = (player.transform.position - transform.position).normalized;
		enemyRigidbody.velocity = direction * chaseSpeed;
	}

	//停止移动
	void Stop(){
		enemyRigidbody.velocity = Vector2.zero;
	}

	//检测与玩家之间的位置关系
	void DetectPlayer(){
		float distance = (transform.position - player.transform.position).magnitude;
		if(distance <= alertDistance && currentStatus == EnemyStatus.Patrol){
			currentStatus = EnemyStatus.WannaAttack;
		}
		if(distance >= alertDistance && (currentStatus == EnemyStatus.WannaAttack || currentStatus == EnemyStatus.AngerAttack)){
			currentStatus = EnemyStatus.LoseAim;
		}
		
	}

	IEnumerator ReleaseWarning(){
		yield return new WaitForSeconds(releaseAfterTime);
		currentStatus = EnemyStatus.Patrol;
	}

	IEnumerator ReleaseAnger(){
		yield return new WaitForSeconds(releaseAfterTime);
		DetectPlayer();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.transform.CompareTag("NormalBullet")){
			currentStatus = EnemyStatus.BeAttack;
		}
	}
}
