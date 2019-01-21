using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollierDetect : MonoBehaviour {

	private GameObject dustObject;
	private void OnCollisionEnter2D(Collision2D other) {
		if(other.transform.CompareTag("Barrier") && other.transform.name.Substring(0, 4) == "Wall"){
			//根据射中的墙判断生成尘土方向
			switch(int.Parse(other.transform.name.Substring(4, 1))){
				case 0:
					dustObject = GameObject.Instantiate(Resources.Load("Prefabs/BulletDust") as GameObject, transform.position, Quaternion.Euler(0, 0, 180));break;
				case 1:
					dustObject = GameObject.Instantiate(Resources.Load("Prefabs/BulletDust") as GameObject, transform.position, Quaternion.Euler(0, 0, 90));break;
				case 2:
					dustObject = GameObject.Instantiate(Resources.Load("Prefabs/BulletDust") as GameObject, transform.position, Quaternion.Euler(0, 0, 0));break;
				case 3:
					dustObject = GameObject.Instantiate(Resources.Load("Prefabs/BulletDust") as GameObject, transform.position, Quaternion.Euler(0, 0, -90));break;
				default:break;
			}
			
			dustObject.transform.position = transform.position;
			transform.parent.gameObject.SetActive(false);
		}
	}
}
