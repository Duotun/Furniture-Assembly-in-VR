using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetChildrenButton: MonoBehaviour {//通过扫描家具的部件建立相应的button

	public GameObject buttonPrefab;
	public GameObject furniture;
	[System.NonSerialized] public static GameObject[] furnitureChildren;//家具物体
	[System.NonSerialized] public static Vector3[,] furnitureOppositePosition;//家具物体相对位置
    private bool controlButton= false;//控制家具部件按钮显示
	[System.NonSerialized] public static bool[] buttonUse;//家具按钮是否使用
	[System.NonSerialized] public static int furnitureChildrenNumber;//家具组件数量
	[System.NonSerialized] public static bool buttonChange;//监测button是否被按下
	private void InitData()//初始化脚本变量数据
	{
		buttonChange = false;
		furnitureChildrenNumber = 0;
		foreach(Transform child in furniture.transform)//获取家具子物体个数
		{
			furnitureChildrenNumber++;
			child.gameObject.AddComponent<BoxCollider>();
		}
		furnitureChildren = new GameObject[furnitureChildrenNumber];
		furnitureOppositePosition = new Vector3[furnitureChildrenNumber,furnitureChildrenNumber];
		buttonUse = new bool[furnitureChildrenNumber];
		Vector3[] objectPosition = new Vector3[furnitureChildrenNumber];
		int i = 0;
		foreach(Transform child in furniture.transform)//名字编号和位置获取
		{
			furnitureChildren[i] = child.gameObject;
			objectPosition[i] = child.position;
			buttonUse[i] = false;
			i++;
			//Debug.Log(child.name+child.position);
			//child.position = new Vector3(1000, 1000, 1000);//把物体移出视野
			//Debug.Log(child.name + child.position);
		}
		for(i=0;i<furnitureChildrenNumber;i++)//计算存储初始的相对位置
		{
			for(int j=0;j<furnitureChildrenNumber;j++)
			{
				furnitureOppositePosition[i, j] = objectPosition[i] - objectPosition[j];
			}
		}
	}
	private void InitButton()//初始化Button按钮布局
	{
		int pos = 1;
		GameObject canvas = GameObject.Find ("Canvas");
		foreach (Transform child in furniture.transform) {
			GameObject button = Instantiate (buttonPrefab);
			button.transform.position = canvas.transform.position + new Vector3 (0, pos * 60, 0);
			button.name = child.name;
			button.AddComponent<ShowFurnitureChildren> ();
			button.transform.Find ("Text").GetComponent<Text> ().text = child.name;
			button.transform.Find ("Text").GetComponent<Text> ().fontSize = 21;
			button.transform.parent = canvas.transform.Find("furniture").transform;
			pos++;
		}
	}
	private void ControlButton(bool enable)//控制Button显示
    {
		int i = 0;
		GameObject canvas = GameObject.Find ("Canvas");
		foreach (Transform child in canvas.transform.Find("furniture").transform) {
			if (buttonUse [i]) {
				child.gameObject.SetActive (false);
			} else {
				child.gameObject.SetActive (enable);
			}
			i++;
		}	
    }
    public void ButtonFunction()
    {
        controlButton = !controlButton;
		ControlButton (controlButton);
    }
	private void ButtonChange()
	{
		ControlButton (controlButton);
	}
	void Awake(){
		InitData ();
		InitButton ();
		ControlButton (false);
	
	}
	void Update(){
		if (buttonChange) {
			ButtonChange ();
			buttonChange = false;
		}
	}
}
