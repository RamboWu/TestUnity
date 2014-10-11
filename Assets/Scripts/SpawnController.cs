using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	public GameObject playerPrefab;
	public Transform spawnObject;

	public void spawnPlayer()
	{
		Network.Instantiate (playerPrefab, spawnObject.position, Quaternion.identity, 0);
	}
}
