using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {

	public void OnClick()
	{
		Application.LoadLevel ("Furniture");
	}
	public void OnClick2()
	{
		Application.LoadLevel ("Demo");
	}
	public void OnClick3()
	{
		Application.Quit ();
	}

}
