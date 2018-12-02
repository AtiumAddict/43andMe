using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusAnimation : Level
{

    private Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (playing && (Input.GetKeyDown("up") || Input.GetKeyDown("w")))
        {
            anim.Play("MinusAnim");
        }
	}
}
