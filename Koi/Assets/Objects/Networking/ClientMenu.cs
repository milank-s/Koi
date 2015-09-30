using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ClientMenu : MonoBehaviour {

	NetworkManager nm;
	void Start () {
		nm = GetComponent<NetworkManager>();
		NetworkClient nc = nm.StartClient();
		nc.Connect("localhost", 7777);
	}
}
