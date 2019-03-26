using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLayerController : MonoBehaviour {

	///<summary>是否为静止物体</summary>
	public bool isStatic;
	private float standardLevel = 0f;
	[SerializeField]
	private Transform footTransform;
	private float depth;
	void Start () {
		if (isStatic)
		{
			depth = (footTransform.position.y - standardLevel) * Mathf.Tan(Mathf.PI / 3);
			transform.position = new Vector3(transform.position.x, transform.position.y, depth);
		}
	}
	

	void Update () {
		if(!isStatic)
		{
			depth = (footTransform.position.y - standardLevel) * Mathf.Tan(Mathf.PI / 3);
			transform.position = new Vector3(transform.position.x, transform.position.y, depth);
		}
	}
}
