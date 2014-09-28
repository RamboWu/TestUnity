using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 1000;
	public GUIText text;
	private int count = 0;

	// Use this for initialization
	void Start () {
		displayText ();
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
			count ++;
			displayText();
			Destroy (other.gameObject);
		}

		Debug.Log ("PlayerController::OnTriggerEntered");
	}

	void displayText()
	{
		text.text = "Count: " + count.ToString ();
	}

}
