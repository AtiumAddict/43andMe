using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusAnimation : MonoBehaviour
{

    private Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}

	void Update ()
    {
		if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            anim.Play("PlusAnim");
        }
	}
}
