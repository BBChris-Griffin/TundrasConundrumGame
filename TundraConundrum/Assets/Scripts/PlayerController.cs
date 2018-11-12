﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float walkingTime;
    public float rotationSpeed;
    public float turnTime;
    private Rigidbody rgb;
    private AdventureGame game;
    private int currDirection; // 0 - forward, 1 - right, 2 - back, 3 - left
    private Quaternion lookRotation;
    private bool turnSet;

	// Use this for initialization
	void Start () {
        turnSet = false;
        currDirection = 0;
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }
        rgb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (game.StartWalking())
        {
            StartCoroutine(WalkThePlayer(game.GetDirection()));
            game.StopWalking();
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
        Debug.Log("Called");
        rgb.velocity = Vector3.zero;
        turnSet = false;
    }
}