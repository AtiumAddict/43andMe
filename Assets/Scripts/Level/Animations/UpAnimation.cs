using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAnimation : MonoBehaviour
{
    private Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}
	
	void Update ()
    {
		if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            anim.Play("UpAnim");
        }
	}
}
