using UnityEngine;
using System.Collections;

public class Radial : MonoBehaviour {

	private bool Move = false;//移动控制变量
	private GameObject gameObj = null;//射线所射向的物体
	private  Vector3 Screen_Space;//屏幕坐标大小
	private Vector3 offset;//物体与相机相对位置差

	public float Speed_X = 1f;//X轴移动速度
	public float Speed_Y = 1f;//Y轴移动速度

	private Vector2 Rotation_0;//旋转时鼠标的初始量
	private Vector2 Rotation_1;//旋转时鼠标的坐标
	private Vector3 Rotation_position;//旋转时物体位置的初始量
	private bool Rotate=false;//旋转控制变量

    public float Speed_view = 15f;//滑轮放大速度
    private float Mini_view = 10f;//相机最小视野
    private float Max_view = 60f;//相机最大视野

    public GameObject GameObj;
    public GameObject MainCamera;
    public GameObject FirstPersonCamera;

    void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			Radial_check ();
		}
		if (Move&&!Rotate) {
			Radial_move ();
		}
		if (Input.GetMouseButtonUp (0)&&Move) {
			//GameObject.Find ("Chair").SendMessage ("Check_position");//移动完毕，检测物体是否组装正确
            Destroy(gameObj.GetComponent<HighlightableObject>());
            gameObj.GetComponent<BoxCollider>().enabled = true;
            Move = false;
		}
		if(gameObj)
		Rotation_camera();

        Zoom_camera();
    }
	void  Radial_check()//射线检测
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
		RaycastHit hitInfo;
		if(Physics.Raycast(ray,out hitInfo))
		{
			//Debug.DrawLine(ray.origin,hitInfo.point);
			gameObj= hitInfo.collider.gameObject;
            Debug.Log("123456");
            if (gameObj&&GameObjectCheck(gameObj)) {//检测是否可以移动
              gameObj.AddComponent<HighlightableObject>();
              gameObj.GetComponent<HighlightableObject>().ConstantOn(Color.red);
              gameObj.GetComponent<BoxCollider>().enabled = false;
			  Screen_Space = Camera.main.WorldToScreenPoint(gameObj.transform.position);//记录下按下鼠标时的相机屏幕坐标
			  offset = gameObj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Screen_Space.z));//记录下相机与物体的相对位置
			  Move = true;
			}
		}
	}
	void Radial_move()//移动物体
	{
		Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Screen_Space.z);
		var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
		gameObj.transform.position = curPosition;
	}
		
	void Rotation_camera()//旋转相机
	{

		if (Input.GetKey (KeyCode.LeftAlt) && Input.GetMouseButtonDown (0)) {
            if (GameObjectCheck(gameObj))
            {
                Rotate = true;
                Rotation_0 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);//记录鼠标初始位置
                MainCamera.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled=false;
                //Debug.Log ("X=="+Rotation_zhou_x);
                //Debug.Log ("Y==" + Rotation_zhou_y);
            }
		}
        if (Rotate)
        {
            if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetMouseButtonUp(0))
            {
                MainCamera.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled=true;
                Rotate = false;
            }
        }
		if (Rotate) {

            Rotation_1 = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 Rotation_c = Rotation_1 - Rotation_0;
			if (Mathf.Abs(Rotation_c.x)> Mathf.Abs(Rotation_c.y)) {
				gameObj.transform.RotateAround(gameObj.transform.position,FirstPersonCamera.transform.up,-Rotation_c.x*Time.deltaTime*Speed_X);//旋转相机
			} else {
				gameObj.transform.RotateAround(gameObj.transform.position,FirstPersonCamera.transform.right,Rotation_c.y*Time.deltaTime*Speed_Y);
			}

			//Debug.Log ("Rotation_X=" + Rotation_X);
			//Debug.Log ("Rotation_Y=" + Rotation_Y);
			//transform.localEulerAngles = new Vector3 (Rotation_Y, Rotation_X, 0);
		}
	}

    void Zoom_camera()
    {
        float Temp_view = -Input.GetAxis("Mouse ScrollWheel") * Speed_view + Camera.main.fieldOfView;
        Camera.main.fieldOfView = Mathf.Clamp(Temp_view, Mini_view, Max_view);
    }
    bool GameObjectCheck(GameObject go)
    {
       // Debug.Log(go.name);
        foreach (Transform child in GameObj.transform)
        {
            //Debug.Log(child.name);
            if (child.name == go.name)
                return true;
        }
        return false;
    }
}

