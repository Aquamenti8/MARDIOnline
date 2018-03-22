using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIcontroller : MonoBehaviour {

    Text statusText, masterText;
	// Use this for initialization
	void Start () {
        statusText = GameObject.Find("StatusText").GetComponent<Text>();
        masterText = GameObject.Find("MasterText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		statusText.text = "Status : " + PhotonNetwork.connectionStateDetailed.ToString();
        masterText.text = "isMasterClient : " + PhotonNetwork.isMasterClient;

    }

}
