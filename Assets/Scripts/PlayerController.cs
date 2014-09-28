using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 1000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal_move = Input.GetAxis("Horizontal");
		float vertical_move = Input.GetAxis("Vertical");

		rigidbody.AddForce(
			new Vector3(horizontal_move*speed*Time.deltaTime,0.0f,vertical_move*speed*Time.deltaTime)
			);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PickUp")
		{
			Destroy (other.gameObject);
		}

		Debug.Log ("PlayerController::OnTriggerEntered");
	}

}
