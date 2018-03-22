using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//usiging UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour {

    public GameObject playerPrefab;
    private Transform target;
    public bool roomJoined = false;

    private float xMax, xMin, yMax, yMin;

    //private Tilemmap tilemap;
    [SerializeField]
    private GameObject map;


    // Use this for initialization
    void Start()
    {


        //UnityScript tScript = GameObject.fin

    }

    void OnJoinedRoom() {
        roomJoined = true;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null && roomJoined==true)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            //target = GameObject.Find("Player(Clone)").transform;

            //Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
            Vector3 minMap = map.transform.position - map.transform.localScale / 2;
            Vector3 maxMap = map.transform.position + map.transform.localScale / 2;

            SetLimits(minMap, maxMap);
        }
		
	}

    private void LateUpdate()
    {
        //if(PhotonNetwork.connectionStateDetailed.ToString()== "Joined"){
        if(target != null) {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMax, xMin),
                Mathf.Clamp(target.position.y, yMin, yMax), -10);
        }
    }

    private void SetLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize; //the height is 2*orthographic size O.O
        float width = height * cam.aspect;

        xMin = minTile.x + width / 2;
        xMax = maxTile.x - width / 2;

        yMin = minTile.y + height / 2;
        yMax = maxTile.y - height / 2;
    }
}
