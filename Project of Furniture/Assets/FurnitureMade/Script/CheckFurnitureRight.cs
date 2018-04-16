using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFurnitureRight : MonoBehaviour {

	private GameObject[] furniture;
	private Vector3[,] oppositePosition;
	private int furnitureNumber;
	public GameObject errorText;
	public GameObject rightText;
	/*priavte bool JudgeFurniture()
	{
		for(int i=0;i<furnitureNumber;i++)
		{
			for(int j=0;j<furnitureNumber;j++)
			{
				if (Vector3.Distance (oppositePosition [i, j], furniture [i].transform.position - furniture [j].transform.position) > 2) {
					return false;
				}
			}
		}
		return true;
		
	}*/

	public void JudgeFurniture(){//监测家具是否安装正确
		for(int i=0;i<furnitureNumber;i++)
		{
			for(int j=0;j<furnitureNumber;j++)
			{
				float dotNumber=Vector3.Dot (oppositePosition [i, j], (furniture [i].transform.position - furniture [j].transform.position));
				Vector3 zero = new Vector3 (0, 0, 0);
				float disNumber = Vector3.Distance (zero, oppositePosition [i, j]) * Vector3.Distance (zero, furniture [i].transform.position - furniture [j].transform.position);
				if (dotNumber / disNumber > -0.1 && dotNumber / disNumber < 0.1) {
					continue;
				} else {
				    
					errorText.SetActive (true);
				
					return;
				}
			
			}
		}
		rightText.SetActive (true);

	}
	void Start()
	{
		furniture = GetChildrenButton.furnitureChildren;
		oppositePosition=GetChildrenButton.furnitureOppositePosition;
		furnitureNumber = GetChildrenButton.furnitureChildrenNumber;
	}
}
