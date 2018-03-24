using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Vector2 direction;
    PhotonView view;
    public Animator anim;

    public bool isControlledPlayer;

    //For Shift
    private GameObject[] targetPoints;
    private GameObject minTarget;
    private Vector2 directionVersPoint;

    // Use this for initialization
    void Start() {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();

        if (view.isMine)
        {
            isControlledPlayer = true;
        }
        else isControlledPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.isMine)
        {
            GetInput();
            GetShift();
            Move();
        }

    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetInput()
    {
            //TODO changer le truc anim.SetInteger, le mettre en fonction du vecteur de direction.
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

    private void GetShift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) //set la target
        {
            Debug.Log("Shift");
            targetPoints = GameObject.FindGameObjectsWithTag("TargetPoint");

            if (targetPoints != null)//si il ey a des cibles dans la salle
            {
                float distance;     //initialise la variable qui contient la distance joueur point
                float minDistance = 100; // est aussi la distance min pour que ce soit reperé!
                minTarget = null;        //rezet la target

                for (int i = 0; i < targetPoints.Length; i++)
                {
                    distance = Vector2.Distance(transform.position, targetPoints[i].transform.position);
                    
                    if (distance < minDistance)
                    {
                        //verifier la direction, si direction et direction vers objt sont trop differents ca passe pas
                        directionVersPoint= new Vector2(transform.position.x- targetPoints[i].transform.position.x, transform.position.y - targetPoints[i].transform.position.y); //pos x2-x1,y2-y1  
                        Debug.Log("DirVerspoint = " + directionVersPoint);
                        Debug.Log("DirVerspointNorm = " + directionVersPoint.normalized);
                        Debug.Log("Move = " + Vector2.MoveTowards(transform.position, targetPoints[i].transform.position, 1000 * Time.deltaTime));
                        Debug.Log("MoveNorm = " + Vector2.MoveTowards(transform.position, targetPoints[i].transform.position, 1000 * Time.deltaTime).normalized);

                        float directionDiff = Vector2.Dot(directionVersPoint.normalized, direction.normalized);
                        Debug.Log("directionDiff" + directionDiff);

                        if (directionDiff < -0.3f)
                        {
                            //calculer la distance du target point au joueur, la plus petite distance restera.
                            minDistance = distance;
                            minTarget = targetPoints[i];
                        }
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.LeftShift)) //se deplace vers la target
        {
            //se deplace vers minTarget
            if (minTarget != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, minTarget.transform.position, 1000 * Time.deltaTime);
            }
        }
        
    }
    //Summary
    /*
     * si on appuit sur shift:
     * on cherche si un point est pres du joueur, en particulier dans la direction du joueur
     * si il y en a un:
     * le joueur n'est plus libre de ses mouvements
     * le joueur se deplace tres rapidement sur le point
     * 
     * 
     */

}
