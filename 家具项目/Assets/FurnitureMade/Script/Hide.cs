using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour {

	public GameObject errorText;
	public GameObject rightText;
	public void OnClick()
	{
		errorText.SetActive (false);
		rightText.SetActive (false);
	}
}
