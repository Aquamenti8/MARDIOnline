using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Vector2 direction;
    PhotonView view;
    public Animator anim;

    // Use this for initialization
    void Start() {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.isMine)
        {
            GetInput();

            Move();
        }

    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        anim.SetInteger("state", 0);
        if (Input.GetKey(KeyCode.Z))
        {
            direction += Vector2.up;
            anim.SetInteger("state", 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            anim.SetInteger("state", 2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            anim.SetInteger("state", 3);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector2.left;
            anim.SetInteger("state", 4);
        }
    }


}
