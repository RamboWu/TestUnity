using UnityEngine;
using System.Collections;

public class SmartFollow : MonoBehaviour {

	//相机面对的物体,角色 
	public GameObject 	target;
	// 设置离人物最远距离.本来想想设个最小距离,但想想wow最近就是第一人称,距离为0.就没设. 
	public float 		MaxDistance = -20f;
	// 是否开始相机碰撞检测, 就是当相机碰到墙,树等物体时是否自动调节位置.
	public bool 		bOpenRay = false;
	// 滚轮拉近 镜头与人物之间距离的速度. 
	public float 		ScrollKeySpeed = 100.0f;
	// 控制相机与人物之间的距离,不能大于 MaxDistance 
	private float 		m_the_distance = -20f;
	// 当碰撞发生后, 将使用这个值来设定相机距离, 
	private float 		HitDistance = 0.0f; 
	// 相机是否碰撞了
	private bool  		bHit = false; 
	// 当相机碰撞后,将把相机放在碰撞点上,怕嵌入墙壁 ,显示墙壁内的画面,就往前提了0.1 
	private float 		AdjuGap = 0.1f; 

	// Use this for initialization
	void Start () {
		transform.position = target.transform.position; 
		transform.rotation = target.transform.rotation; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() 
	{ 
		//打开碰撞检测 
		if(bOpenRay) 
			AutoRegulationPos(); 
		
		// 滚轮设置 相机与人物的距离.   
		if(Input.GetAxis("Mouse ScrollWheel") != 0) 
		{ 
			m_the_distance = m_the_distance + Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ScrollKeySpeed;       
		} 
		// 鼠标中间滚动得到的值是不确定的,不会正好就是0,或 -20,当大于0时就设距离为0,小于MaxDistance就设置为MaxDistance 
		if(m_the_distance>0) 
			m_the_distance = 0; 
		if(m_the_distance < MaxDistance) 
			m_the_distance = MaxDistance; 
		
		float tmpY; 
		//当按下鼠标右键,以人物为中心,旋转相机. 
		if(!Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Mouse0)) 
		{ 
			transform.position = target.transform.position; 
			// 乘 -1 是因为上下移动的习惯,鼠标下拉,镜头是往上走还是往下走. 个人喜好. 可以设个公有变量来调节. 
			tmpY = transform.eulerAngles.x + Input.GetAxis("Mouse Y") * -1; 
			//不让镜头从头顶,或脚下直接转过去, 以下都一样. 
			if(tmpY > 85&&tmpY < 90 ) 
				tmpY = 85; 
			else if(tmpY > 265&& tmpY < 270) 
				tmpY = 275; 
			transform.eulerAngles = new Vector3(tmpY ,transform.eulerAngles.y + Input.GetAxis("Mouse X") , transform.eulerAngles.z ); 
			SetDistance(); 
		} 
		//当没有鼠标按下的时候 
		else 
		{ 
			transform.position = target.transform.position; 
			SetDistance(); 
		} 
	}

	//设置相机与人物之间的距离 
	void SetDistance() 
	{ 
		//当碰撞发生时,调整的距离变成 HitDistance 
		if(bHit) 
		{ 
			transform.Translate(Vector3.forward * HitDistance); 
		} 
		else 
		{ 
			transform.Translate(Vector3.forward * m_the_distance); 
		} 
	} 
	//碰撞检测.并自动调节. 
	void AutoRegulationPos() 
	{ 
		RaycastHit hit; 
		float tmpValue = m_the_distance; 
		if(m_the_distance < 0) 
			tmpValue = -1 * tmpValue; 
		else if(m_the_distance > 0) 
			tmpValue = 0; 

		if(Physics.Raycast(target.transform.position, transform.TransformDirection(-Vector3.forward), out hit, tmpValue)) 
		{ 
			bHit = true; 
			HitDistance = -Vector3.Distance(target.transform.position,hit.point ) + AdjuGap ; 
			if(HitDistance > 0) 
				HitDistance = 0.0f; 
		} 
		else    if(bHit) 
		{ 
			bHit = false; 
			HitDistance = 0.0f; 
		} 
	}
}
