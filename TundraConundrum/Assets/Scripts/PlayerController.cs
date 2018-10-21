using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float walkingTime;
    private Rigidbody rgb;
    private AdventureGame game;

	// Use this for initialization
	void Start () {
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
            StartCoroutine(WalkThePlayer());
            game.StopWalking();
        }
    }

    private IEnumerator WalkThePlayer()
    {
        rgb.velocity = Vector3.forward * speed;
        yield return new WaitForSeconds(walkingTime);
        rgb.velocity = Vector3.zero;
    }
}
