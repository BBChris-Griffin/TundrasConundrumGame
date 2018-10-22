using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float walkingTime;
    private Rigidbody rgb;
    private AdventureGame game;
    private int currDirection; // 0 - forward, 1 - right, 2 - back, 3 - left

	// Use this for initialization
	void Start () {
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
    }

    private IEnumerator WalkThePlayer(int direction)
    {
        currDirection += direction;

        if (currDirection % 4 == 1)
        {
            rgb.velocity = Vector3.right * speed;
            transform.rotation = Quaternion.LookRotation(Vector3.right);
        }
        else if (currDirection % 4 == 3)
        {
            rgb.velocity = Vector3.left * speed;
            transform.rotation = Quaternion.LookRotation(Vector3.left);
        }
        else if (currDirection % 4 == 0)
        {
            rgb.velocity = Vector3.forward * speed;
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else
        {
            rgb.velocity = Vector3.back * speed;
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
       
        yield return new WaitForSeconds(walkingTime);
        rgb.velocity = Vector3.zero;
    }
}
