using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    private bool fade;
	// Use this for initialization
	void Start () {
        fade = false;
        StartCoroutine(showOff());
	}
	
	// Update is called once per frame
	void Update () {
        if(fade)
        {
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(GetComponent<CanvasGroup>().alpha, 0.0f, 0.025f);
            if (GetComponent<CanvasGroup>().alpha <= 0.1f)
            {
                gameObject.SetActive(false);
            }
        }     
    }

    private IEnumerator showOff()
    {
        yield return new WaitForSeconds(1f);
        fade = true;
    }
}
