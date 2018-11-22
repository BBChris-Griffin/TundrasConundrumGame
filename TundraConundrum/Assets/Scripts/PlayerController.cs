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
    private Rigidbody rgb;
    private AdventureGame game;
    private int currDirection; // 0 - forward, 1 - right, 2 - back, 3 - left
    private Quaternion lookRotation;
    private bool turnSet;
    private GameObject tundra;
    private Quaternion currentLook;
    private bool lookBegan;
    private GameObject lightDoor;

    // Use this for initialization
    void Start () {
        turnSet = false;
        currDirection = 0;
        tundra = GameObject.FindGameObjectWithTag("Tundra");
        lightDoor = GameObject.FindGameObjectWithTag("Door To The Light");
        lookBegan = false;
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }
        rgb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if(game.Failure())
        {
            StareAtTundra();
        }
        else if(game.Victory())
        {
            Debug.Log("Victory");
            StartCoroutine(Victory());
        }
        if (game.StartWalking())
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
            }
        }

        if(turnSet)
        {
          transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator WalkThePlayer(int direction)
    {
        currDirection += direction;
        Quaternion rotation = transform.rotation;
        Vector3 walkTowards = Vector3.zero;

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
    }

    private void StareAtTundra()
    {
        transform.LookAt(tundra.transform);
        //transform.rotation = Quaternion.Slerp(transform.rotation, tundra.transform.rotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Victory()
    {
        lightDoor.transform.parent = null;
        transform.eulerAngles.Set(Vector3.up.x, Vector3.up.y, Vector3.up.z);
        rgb.velocity = Vector3.up * flySpeed;
        yield return new WaitForSeconds(flyTime);
    }

    public bool GetTurnSet()
    {
        return turnSet;
    }
}
