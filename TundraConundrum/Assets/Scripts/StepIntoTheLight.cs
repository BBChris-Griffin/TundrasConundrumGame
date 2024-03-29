﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepIntoTheLight : MonoBehaviour {

    public GameObject winUI;
    private AdventureGame game;
    private GameObject player;
    private AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audio.Play();
            winUI.SetActive(true);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(winUI.activeSelf)
        {
            winUI.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(winUI.GetComponent<CanvasGroup>().alpha, 1f, 0.1f);
        }
    }
}
