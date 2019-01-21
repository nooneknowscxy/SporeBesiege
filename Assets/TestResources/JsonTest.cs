using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonTest : MonoBehaviour {

	void Start () {
		string json = File.ReadAllText("/FirstProp.json", Encoding.UTF8);
		PropModel test = JsonUtility.FromJson<PropModel>(json);
	}
	
}
