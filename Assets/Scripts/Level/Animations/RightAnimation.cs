﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAnimation : MonoBehaviour
{

    private Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}
	
	void Update ()
    {
		if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            anim.Play("RightAnim");
        }
	}
}
