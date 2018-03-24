using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    /*
     * le gun suis le personnage, et suit l'angle de la souris
     * 
     * 
     * 
     */
    public GameObject player;
    public GameObject[] playerList;
    
	// Use this for initialization
	void Start () {
        /*set le player en fonction de qui est le joueur courant (check les instance
         * de player et regarde la variable "isControlledPlayer"
         * si c'est le cas prend le gameobject et attache le gun a aca
         */
        //PhotonView.isMine;
        
    }
	
	// Update is called once per frame
	void Update () {
        if(player == null)          //GET PLAYER
        {
            playerList = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < playerList.Length; i++)
            {
                if (playerList[i].GetComponent<PlayerController>().isControlledPlayer)
                {
                    player = playerList[i];
                }
            }
            if (player != null)
            {
                transform.position = player.transform.position;
                transform.parent = player.transform;
            }
        }
        if (player != null)       //ROTATE TO MOUSE
            {

            FaceMouse();
        }
    }

    void FaceMouse()
    {
        /*
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(Input.mousePosition.y - transform.position.y, Input.mousePosition.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        */

        Vector3 mousePosition = Input.mousePosition;
        Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

        transform.right = direction;
    }

    
}
