using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float walkingTime;
    public float rotationSpeed;
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
        Quaternion rotation = transform.rotation;

        if (currDirection % 4 == 1 || currDirection % 4 == -3)
        {
            rgb.velocity = Vector3.right * speed;
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(rotation, Quaternion.Euler(rotation.x, rotation.y + 90, rotation.z), rotationSpeed);
        }
        else if (currDirection % 4 == 3 || currDirection % 4 == -1)
        {
            rgb.velocity = Vector3.left * speed;
            transform.rotation = Quaternion.Slerp(rotation, Quaternion.Euler(rotation.x, rotation.y - 90, rotation.z), rotationSpeed);
        }
        else if (currDirection % 4 == 0 || currDirection % 4 == -2)
        {
            rgb.velocity = Vector3.forward * speed;
            //transform.rotation = Quaternion.Slerp(rotation, Quaternion.Euler(rotation.x, rotation.y, rotation.z), rotationSpeed);
        }
        else if (currDirection % 4 == 2 || currDirection % 4 == -0)
        {
            rgb.velocity = Vector3.back * speed;
            transform.rotation = Quaternion.Slerp(rotation, Quaternion.Euler(rotation.x, rotation.y + -180, rotation.z), rotationSpeed);
        }

        yield return new WaitForSeconds(walkingTime);
        rgb.velocity = Vector3.zero;
    }
}
