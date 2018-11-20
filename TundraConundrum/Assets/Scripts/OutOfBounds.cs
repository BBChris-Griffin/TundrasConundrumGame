﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

	public Material screenEffect;
	public float blurValue = 0.025f;
	private bool outOfBounds;
	private bool descent;
	private float magValue;
	// Use this for initialization
	void Start ()
	{
			outOfBounds = false;
			descent = false;
			magValue = 0.0f;
			screenEffect.SetFloat("_Magnitude", 0.0f);
	}

	// Update is called once per frame
	void Update ()
	{
		if(outOfBounds)
		{
			screenEffect.SetFloat("_Magnitude", magValue);

			if(magValue < 1.0f && !descent)
			{
				magValue += blurValue;
			}
			else if(magValue >= 1.0f && !descent)
			{
				descent = true;
			}
			else if(magValue > 0.0f && descent)
			{
				magValue -= blurValue;
			}
			else if(magValue <= 0.0f && descent)
			{
				magValue = 0.1f;
				descent = false;
				outOfBounds = false;
				screenEffect.SetFloat("_Magnitude", 0.0f);
			}
		}
	}

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggered the Exitted");
				outOfBounds = true;
    }
}
