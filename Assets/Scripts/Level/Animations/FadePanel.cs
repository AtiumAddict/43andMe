using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour {
    
	void Start ()
    {
        Invoke("DisableSelf", 2f);
    }

	void DisableSelf ()
    {
        this.gameObject.SetActive(false);
	}
}
