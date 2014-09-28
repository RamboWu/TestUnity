using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

		offset = this.transform.position;

	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;

		float mouse_x = Input.GetAxis ("Mouse X");
		float mouse_y = Input.GetAxis ("Mouse Y");


		Vector3 vec = Vector3.up;

		if (mouse_x != 0.0f) 
		{
			transform.Rotate(new Vector3(0,mouse_x,0));
			//Debug.Log ("Mouse X:" + mouse_x + "Mouse Y:" + mouse_y);
		}
		//transform.Rotate(
	}
}
