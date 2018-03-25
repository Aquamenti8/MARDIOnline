﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class GunController : MonoBehaviour {

    /*
     * le gun suis le personnage, et suit l'angle de la souris
     * 
     * 
     * 
     */
    public GameObject player;
    public GameObject[] playerList;
    public GameObject laser;
    public float laserLength;
    float angleShoot;

    
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
        
        if (player == null)          //GET PLAYER
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
            
            GetGunFire();
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
                mousePosition.x - player.transform.position.x,
                mousePosition.y - player.transform.position.y
            );

        transform.right = direction;
    }

    void GetGunFire()
    {
        /*
         * si click gauche est pressé
         * Tire un laser dans la direction de la souris
         */
         if (Input.GetMouseButtonDown(0))
        {
            //Tire un laser en direction de la souri!
            Vector3 mousePosition = Input.mousePosition;
            Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );
            angleShoot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            laserLength = Vector3.Distance(transform.position, mousePosition);

            GameObject laserInstance = (GameObject)Instantiate(laser, transform.position, Quaternion.Euler(0, 0, angleShoot));
            laserInstance.transform.localScale = new Vector3(laserLength, 100, 1);

        }
    }
    
}
