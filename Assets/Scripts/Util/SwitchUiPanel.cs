using UnityEngine;
using System.Collections;

public class SwitchUiPanel : MonoBehaviour {

	public GameObject panel_source;
	public GameObject panel_target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeFromA2B()
	{
		if (panel_source != null && panel_target != null)
		{
			panel_source.SetActive(false);
			panel_target.SetActive(true);
		}
	}
}
