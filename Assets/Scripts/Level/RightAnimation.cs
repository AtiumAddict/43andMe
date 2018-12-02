using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAnimation : Level
{

    private Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (playing && (Input.GetKeyDown("right") || Input.GetKeyDown("d")))
        {
            anim.Play("RightAnim");
        }
	}
}
