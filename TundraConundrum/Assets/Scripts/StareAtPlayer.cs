using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareAtPlayer : MonoBehaviour {

    public float jawSpeed;
    public float dropTime;
    public ParticleSystem beam;
    private GameObject player;
    private AdventureGame game;
    private bool gameOver;
    private bool fuckEmUp;
    private GameObject gameUI;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        audio = GetComponent<AudioSource>();
        gameOver = false;
        fuckEmUp = false;
        beam.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);

        if(game.Failure() && !gameOver)
        {
            gameOver = true;
            StartCoroutine(Attack());
        }
	}

    IEnumerator Attack()
    {
        gameUI.SetActive(false);
        GameObject jaw = transform.GetChild(0).gameObject;
        jaw.GetComponent<Rigidbody>().velocity = Vector3.down * jawSpeed;
        audio.Play();
        yield return new WaitForSeconds(dropTime);
        fuckEmUp = true;
        beam.Play();
        jaw.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
