using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float walkingTime;
    public float rotationSpeed;
    public float turnTime;
    public float flyTime;
    public float flySpeed;
    private Vector3 walkTowards;
    private Rigidbody rgb;
    private AdventureGame game;
    private int currDirection; // 0 - forward, 1 - right, 2 - back, 3 - left
    private Quaternion lookRotation;
    private bool turnSet;
    private GameObject tundra;
    private Quaternion currentLook;
    private bool lookBegan;
    private GameObject lightDoor;
    private bool victory;
    private GameObject gameUI;
    private bool walking;

    // Use this for initialization
    void Start () {
        turnSet = false;
        currDirection = 0;
        victory = false;
        walking = false;
        tundra = GameObject.FindGameObjectWithTag("Tundra");
        lightDoor = GameObject.FindGameObjectWithTag("Door To The Light");
        lookBegan = false;
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }
        rgb = GetComponent<Rigidbody>();
        walkTowards = Vector3.zero;
    }

    private void Update()
    {

        if(game.Failure())
        {
            StareAtTundra();
        }
        else if(game.Victory() && !victory)
        {
            gameUI.SetActive(false);
            victory = true;
            StartCoroutine(Victory());
        }
        else if (game.StartWalking())
        {
            StartCoroutine(WalkThePlayer(game.GetDirection()));
            game.StopWalking();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!lookBegan)
                {
                    currentLook = transform.rotation;
                    lookBegan = true;
                }
                StareAtTundra();
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                transform.rotation = currentLook;
                lookBegan = false;
                gameUI.SetActive(true);
            }
        }

        if(turnSet)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            //if(game.Victory())
            //{
            //    lightDoor.transform.parent = null;
            //}
        }
    }

    private IEnumerator WalkThePlayer(int direction)
    {
        walking = true;
        gameUI.SetActive(false);
        currDirection += direction;
        Quaternion rotation = transform.rotation;
        walkTowards = Vector3.zero;

          if (currDirection % 4 == 1 || currDirection % 4 == -3)
          {
              walkTowards = Vector3.right;
          }
          else if (currDirection % 4 == 3 || currDirection % 4 == -1)
          {
              walkTowards = Vector3.left;
          }
          else if (currDirection % 4 == 0 || currDirection % 4 == -0)
          {
              walkTowards = Vector3.forward;
          }
          else if (currDirection % 4 == 2 || currDirection % 4 == -2)
          {
              walkTowards = Vector3.back;
          }
          lookRotation = Quaternion.LookRotation(walkTowards);
          turnSet = true;

        yield return new WaitForSeconds(turnTime);

        rgb.velocity = walkTowards * speed;
        yield return new WaitForSeconds(walkingTime);
        rgb.velocity = Vector3.zero;
        turnSet = false;
        walking = false;
        gameUI.SetActive(true);
    }

    private void StareAtTundra()
    {
        transform.LookAt(tundra.transform);
        gameUI.SetActive(false);
        //transform.rotation = Quaternion.Slerp(transform.rotation, tundra.transform.rotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Victory()
    {
        //Vector3 up = new Vector3(-90.0f, transform.rotation.y, transform.rotation.z);
        //lookRotation = Quaternion.LookRotation(Vector3.up);
        //turnSet = true;
        lightDoor.transform.parent = null;
        tundra.SetActive(false);
        transform.Rotate(-90.0f, transform.rotation.y, transform.rotation.z);
        rgb.velocity = Vector3.up * flySpeed;
        yield return new WaitForSeconds(flyTime);
        turnSet = false;

    }

    public bool GetTurnSet()
    {
        return turnSet;
    }

    public bool IsWalking()
    {
        return walking;
    }
}
