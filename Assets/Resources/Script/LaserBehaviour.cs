using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    public float timeToDeath;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeToDeath -= 1 * Time.deltaTime;
        if(timeToDeath <= 0){
            Destroy(gameObject);
        }
	}
}
