using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAnimation : Level
{

    private Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (playing && (Input.GetKeyDown("left") || Input.GetKeyDown("a")))
        {
            anim.Play("LeftAnim");
        }
	}
}
