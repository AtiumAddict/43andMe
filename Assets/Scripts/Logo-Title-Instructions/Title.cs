using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

	void Start ()
    {
        Invoke("NextScene", 4f);
    }
	
	void NextScene()
    {
        SceneManager.LoadScene("01Instructions");
    }
}
