using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GunStatus
{
	Normal,
	PropNormal,
	Reload,
	Fire,
	PropFire
}
public class GunManager : MonoBehaviour {
	private GunStatus currentStatus;	//枪当前状态
	public Bullet[] bullets;
	private int currentNum, specialNum;
	public int BulletNumber{get{return currentNum;}set{currentNum = value;}}
	public GameObject[] bulletPrefabs;	//子弹预制体
	private GameObject[] bulletObjects, exBulletObjs;
	private Rigidbody2D[] bulletRigids, exBulletRigs;
	public int bulletsNum = 5;
	public float reloadDelay = 0.5f;
	private float nextShoot = 0.0f; 
	public float shootDelay = 1.0f;	
	private Vector2 mousePos2World, shootDir;
	public static GunManager Instance{get; private set;}
	public bool testMode = false;
	private void Awake() {
		Instance = this;
	}

	private void Start() {
		MakeBullets();
		currentStatus = GunStatus.Normal;
	}

	private void Update() {
		mousePos2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(Input.GetButtonDown("Fire1") && Time.time > nextShoot){
			nextShoot = Time.time + shootDelay;
			if(currentStatus == GunStatus.Normal){
				currentStatus = GunStatus.Fire;
				StartCoroutine(RecoverNormalStatus());
			}else if(currentStatus == GunStatus.PropNormal)
			{
				currentStatus = GunStatus.PropFire;
				StartCoroutine(RecoverNormalStatus());
			}
		}

		if(Input.GetKeyDown(KeyCode.R) && currentStatus != GunStatus.PropNormal)
		{
			currentStatus = GunStatus.Reload;
			//装弹动画
			Animator reloadAnimator = GameObject.Find("ReloadProgress").GetComponent<Animator>();
			reloadAnimator.SetTrigger("ReloadStart");
		}

		//开发测试
		if(testMode){
			//道具测试
			if(Input.GetKeyDown(KeyCode.E)){
				ChangeAfterBullet(1, 3);
			}
		}

		//有限状态机
		switch(currentStatus){
			case GunStatus.Fire:
				//获得射击方向
				shootDir = (mousePos2World - (Vector2)transform.position).normalized;
				Launch(shootDir);
				break;
			case GunStatus.PropFire:
				shootDir = (mousePos2World - (Vector2)transform.position).normalized;
				SpecialLaunch(shootDir);
				break;
			case GunStatus.Reload:
				//当弹匣容量满时不能装弹
				if(currentNum < bulletsNum){
					StartCoroutine(WaitReload());
				}else
				{
					currentStatus = GunStatus.Normal;
				}
				break;
			case GunStatus.PropNormal:
				//如果特殊子弹耗尽，转换为Normal状态
				if(specialNum == 0){
					currentStatus = GunStatus.Normal;
				}
				break;
			default:break;
		}
		Debug.Log("CurrentStatue = " + currentStatus);
	}

	void MakeBullets(){
		bullets = new Bullet[bulletsNum];
		bulletObjects = new GameObject[bulletsNum];
		bulletRigids = new Rigidbody2D[bulletsNum];
		//设置当前子弹数为满
		currentNum = bulletsNum;
		for (int i = 0; i < bulletsNum; i++)
		{
			bullets[i] = new Bullet();
			//设置子弹物体的预制体
			bulletObjects[i] = GameObject.Instantiate(bulletPrefabs[(int)bullets[i].Type]);
			bulletRigids[i] = bulletObjects[i].GetComponent<Rigidbody2D>();
			//设置子弹物体位置和父物体（发射后脱离父物体）
			bulletObjects[i].transform.position = this.transform.position;
			bulletObjects[i].transform.parent = this.transform;
			bulletObjects[i].SetActive(false);
		}
	}

	//根据方向发射子弹
	void Launch(Vector2 direction){
		if(currentNum > 0){
			bulletObjects[currentNum - 1].SetActive(true);
			//与父物体解绑
			bulletObjects[currentNum - 1].transform.parent = null;
			bulletRigids[currentNum - 1].velocity = direction * bullets[currentNum - 1].Speed;
			//子弹数减一
			currentNum -= 1;	
		}
	}

	void SpecialLaunch(Vector2 direction){
		if(specialNum > 0){
			exBulletObjs[specialNum - 1].SetActive(true);
			//与父物体解绑
			exBulletObjs[specialNum - 1].transform.parent = null;
			exBulletRigs[specialNum - 1].velocity = direction * bullets[specialNum - 1].Speed;
			//子弹数减一
			specialNum -= 1;	
		}
	}

	void Reload(){
		int empty = bulletsNum - currentNum;
		for (int i = 0; i < empty; i++){
			//设置子弹物体位置和父物体（发射后脱离父物体）
			bulletObjects[currentNum + i].transform.position = this.transform.position;
			bulletObjects[currentNum + i].transform.parent = this.transform;
			bulletObjects[currentNum + i].SetActive(false);
		}
		currentNum = bulletsNum;
	}

	//id为子弹类型BulletType中的索引值, num为特殊子弹数量
	void ChangeAfterBullet(int id, int num){
		currentStatus = GunStatus.PropNormal;
		specialNum = num;
		exBulletObjs = new GameObject[num];
		exBulletRigs = new Rigidbody2D[num];
		for (int i = 0; i < num; i++)
		{
			exBulletObjs[i] = GameObject.Instantiate(bulletPrefabs[id]);
			exBulletRigs[i] = exBulletObjs[i].GetComponent<Rigidbody2D>();
			exBulletObjs[i].transform.position = this.transform.position;
			exBulletObjs[i].transform.parent = this.transform;
			exBulletObjs[i].SetActive(false);
		}
		
	}

	IEnumerator WaitReload(){
		yield return new WaitForSeconds(reloadDelay);
		Reload();
		yield return RecoverNormalStatus();
	}

	IEnumerator RecoverNormalStatus(){
		yield return new WaitForEndOfFrame();
		if(currentStatus == GunStatus.Fire || currentStatus == GunStatus.Reload){
			currentStatus = GunStatus.Normal;
		}else if(currentStatus == GunStatus.PropFire)
		{
			currentStatus = GunStatus.PropNormal;
		}
	}

}
