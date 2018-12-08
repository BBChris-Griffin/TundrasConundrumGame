using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBGMusic : MonoBehaviour {

	public AudioClip[] bg;
	private AudioSource audioPlayer;
	// Use this for initialization
	void Start () {
		audioPlayer = GetComponent<AudioSource>();
		audioPlayer.clip = bg[Random.Range(0, bg.Length)];
		audioPlayer.Play();
	}

	// Update is called once per frame
	void Update () {

	}
}
