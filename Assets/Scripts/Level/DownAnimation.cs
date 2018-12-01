using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownAnimation : MonoBehaviour {

    private Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            anim.Play("DownAnim");
        }
	}
}
