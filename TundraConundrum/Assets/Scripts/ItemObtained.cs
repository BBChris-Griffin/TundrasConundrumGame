using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObtained : MonoBehaviour {

    public float speed;
    public float rotateValue;
    public float moveTime;
    public float displayTime;
    private Rigidbody rgb;
    private GameObject player;

    // Use this for initialization
    void Start () {
        rgb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

    private void Update()
    {
        StartCoroutine(Congratulations());
    }


    private IEnumerator Congratulations()
    {
        Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
        rgb.angularVelocity = new Vector3(0.0f, rotateValue, 0.0f);

        // Rise to the Visible Screen
        rgb.velocity = new Vector3(playerVelocity.x, playerVelocity.y + speed, playerVelocity.z);
        yield return new WaitForSeconds(moveTime);
        rgb.velocity = Vector3.zero;
        
        // Spin in Place
        yield return new WaitForSeconds(displayTime);
        Destroy(this.gameObject);

    }
}
