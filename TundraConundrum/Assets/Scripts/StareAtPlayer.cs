using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareAtPlayer : MonoBehaviour {

    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
	}
}
