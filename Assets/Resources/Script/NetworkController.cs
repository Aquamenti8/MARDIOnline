using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{

    private string _gameVersion = "0.1";
    public GameObject playerPrefab;
    public bool player_spawned = false;
    

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("Status : " + PhotonNetwork.connectionStateDetailed.ToString());
    }


    /*****méthode callback offertes par Photon ****/
    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room. Initialise room creation");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("We just joined a room");
        PhotonNetwork.Instantiate("Prefab/" + playerPrefab.name, playerPrefab.transform.position, Quaternion.identity, 0);
        player_spawned = true;
        Debug.Log("PlayerSpwaned!");
    }
}
