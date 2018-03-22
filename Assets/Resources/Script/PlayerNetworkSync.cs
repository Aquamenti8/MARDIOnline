using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PlayerNetworkSync : MonoBehaviour {

    private Vector3 _correctPlayerPos;
    private Quaternion _correctPlayerRot;
    private PhotonView _view;

    //Update is called once per frame
    void Start()
    {
        _view = GetComponent<PhotonView>();
        
    }
    void Update()
    {
        if (!_view.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this._correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, this._correctPlayerRot, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializePreview(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            this._correctPlayerPos = (Vector3)stream.ReceiveNext();
            this._correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
