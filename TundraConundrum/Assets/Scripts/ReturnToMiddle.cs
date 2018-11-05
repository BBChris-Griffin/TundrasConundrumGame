using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMiddle : MonoBehaviour {

    public Transform respawn;

	// Use this for initialization
	void Start () {
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = respawn.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("Player");
        }
    }
}
