using UnityEngine;
using System.Collections;

public class Camera_move : MonoBehaviour {

	public float Speed_X = 5f;//相机视角移动速度
	public float Speed_Y = 5f;
	public float Speed_view=7f;//相机视野

	private float Mini_Y=-60f;//相机最小视野
	private float Max_Y=60f;//相机最大视野
	private float Mini_X=-80f;//相机最小视野
	private float Max_X=80f;//相机最大视野
	private float Mini_view=20f;//相机最小视野
	private float Max_view=130f;//相机最大视野


	private float Rotation_Y = 0f;
	private float Rotation_y = 0f;
	private float Rotation_X = 0f;
	private float Rotation_x = 0f;
	private float Rotation_Z=0f;
	private bool Rotate=false;
	void Start()
	{
	}

	void Update()
	{
		//Rotation_camera ();//相机视角旋转
		Zoom_camera ();//鼠标滑轮放大与缩小
	}

	/*void Rotation_camera()
	{
		if (Input.GetMouseButtonDown (1)) {
			Rotate = true;
			Rotation_X = transform.localEulerAngles.y;
			Rotation_x = transform.localEulerAngles.y;

			Rotation_Y = -transform.localEulerAngles.x;
			Rotation_y = -transform.localEulerAngles.x;

			Rotation_Z = transform.localEulerAngles.z;
		}
		if (Input.GetMouseButtonUp (1)) {
			Rotate = false;
		}
		if (Rotate) {
			Rotation_X = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * Speed_X;
			Rotation_Y +=Input.GetAxis ("Mouse Y") * Speed_Y;
			Rotation_X = Mathf.Clamp (Rotation_X, Rotation_x+Rotation_y+Mini_X, Rotation_x+Rotation_y+Max_X);
			Rotation_Y = Mathf.Clamp (Rotation_Y, Rotation_y+Mini_Y, Rotation_y+Max_Y);
			transform.localEulerAngles = new Vector3 (-Rotation_Y, Rotation_X, Rotation_Z);
		}
	}*/
	void Zoom_camera()
	{
		float Temp_view = -Input.GetAxis ("Mouse ScrollWheel") * Speed_view+Camera.main.fieldOfView;
		Camera.main.fieldOfView = Mathf.Clamp (Temp_view, Mini_view, Max_view);
	}
}
