using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFurnitureChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button button= this.GetComponent<Button>();
		button.onClick.AddListener (OnClick);
	}
	private void OnClick()//监听相应button是否被按下
	{
		for (int i = 0; i < GetChildrenButton.furnitureChildrenNumber; i++) {
			if (this.name == GetChildrenButton.furnitureChildren[i].name) {
				GetChildrenButton.buttonUse [i] = true;
				GetChildrenButton.buttonChange = true;
				GetChildrenButton.furnitureChildren [i].transform.position = new Vector3 (10, 1, 16);
				return;
			}
		}
	}
}
