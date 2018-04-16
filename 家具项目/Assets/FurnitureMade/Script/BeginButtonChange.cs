using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginButtonChange : MonoBehaviour {

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device controller
	{
		get{
			return SteamVR_Controller.Input ((int)trackedObject.index);
		}
	}
	public GameObject trainingMenu;

	private void Start()
	{
		trackedObject = this.GetComponent<SteamVR_TrackedObject> ();
	}
	public void TrainingChange()
	{
		trainingMenu.transform.position= controller.transform.pos;
	}
     
}
