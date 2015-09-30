using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ServerMenu : MonoBehaviour {
	NetworkManager nm;
	void Awake () {
		nm = GetComponent<NetworkManager>();
	}

	public void CreateServer(){
		nm.StartServer();
	}
}
