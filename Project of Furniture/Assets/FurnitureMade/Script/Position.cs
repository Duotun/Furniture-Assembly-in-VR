using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
	
	private Vector3[,] related;//记录物体部件相对位置
	// Use this for initialization
	void Start () {
		record ();
	}

	// Update is called once per frame
	void Update () {

	}
	void record()//记录函数
	{
		//record related positions
		Vector3[] now=new Vector3[30];//
		int cnt = 0;
		foreach (Transform child in transform) {
			now [cnt++] = child.position;
		}
		related=new Vector3[cnt,cnt];
		int i = 0;
		for (; i < cnt; i++) {
			// use the array to recored the related position
			for (int j = i+1; j < cnt; j++) {
				related [i, j] = now [j] - now [i];
			}
		}
		foreach (Transform child in transform) {
            // initiate the collider
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.AddComponent<BoxCollider>();
		}

	}
	void Check_position()//最终检测函数，检测物体是否组装正确
	{
		Vector3[] now=new Vector3[30];
		int cnt = 0;
		foreach (Transform child in transform) {
			now [cnt++] = child.position;
		}
		int i = 0;
		for (; i < cnt; i++) {
			// use the array to recored the related position
			for (int j = i+1; j < cnt; j++) {
				if (Distance (related [i, j] - (now [j] - now [i])) > 2) {
					Debug.Log ("Error!");//输出
					return;
				}
			}
		}
		Debug.Log ("Right!");//输出
	}
	double Distance(Vector3 a)//计算距离
	{
		return Mathf.Sqrt (a.x * a.x + a.y * a.y + a.z * a.z);
	}
}
