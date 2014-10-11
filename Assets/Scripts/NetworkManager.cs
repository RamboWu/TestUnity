using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	public List<EventDelegate> onStartGame = new List<EventDelegate>();

	public string GameName = "CGCookie_Networking";
	private bool refreshing = false;	
	private HostData[] m_host_data;
	//开启服务器
	public void startServer()
	{
		Debug.Log ("Starting Server");
		//Debug.Log("Network have public address:"+Network.HavePublicAddress ());
		Network.InitializeServer (32, 25001, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (GameName, "Tutorial Game Name", "This is a hello world game!");
	}

	public void closeServer()
	{

		Network.Disconnect ();
		MasterServer.UnregisterHost ();
	}

	//链接服务器
	public void connect2Server()
	{
		refreshHostList ();
	}

	void refreshHostList()
	{
		MasterServer.RequestHostList (GameName);
		Debug.Log ("refreshHostList");
		refreshing = true;
	}

	public void clearHostList()
	{
		MasterServer.ClearHostList ();
	}	

	void Update(){
		if (refreshing) {
			if (MasterServer.PollHostList().Length>0)
			{
				refreshing = false;
				Debug.Log(MasterServer.PollHostList().Length);
				m_host_data =  MasterServer.PollHostList();
				NetworkConnectionError err = Network.Connect( m_host_data[0] );
				Debug.Log(err);
			}
		}
	}

	void OnServerInitialized()
	{
		Debug.Log ("Server initialized");
		EventDelegate.Execute(onStartGame);
	}

	void OnConnectedToServer()
	{
		Debug.Log ("OnConnectedToServer");
		EventDelegate.Execute(onStartGame);
	}

	void OnMasterServerEvent(MasterServerEvent mse)
	{
		if (mse == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log("Registered Server!");
		}
	}


}
