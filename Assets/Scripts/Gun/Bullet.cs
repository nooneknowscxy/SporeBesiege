using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
	Normal,	//普通子弹
	Fire,	//火弹
	Water,	//水弹
	Grenade	//榴弹
}

public class Bullet{
	private float size;	//子弹大小
	private float speed;	//子弹速度
	private BulletType type;	//子弹种类
	public float Size{get{return size;}set{size = value;}}
	public BulletType Type{get{return type;}set{type = value;}}
	public float Speed{get{return speed;}set{speed = value;}}
	public Bullet(float sizeInput = 1.0f, float speedInput = 12.0f, BulletType typeInput = BulletType.Normal){
		this.size = sizeInput;
		this.speed = speedInput;
		this.type = typeInput;
	}
}
