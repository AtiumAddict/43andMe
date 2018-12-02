using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{

	void Start ()
    {
        Invoke("NextScene", 12f);
    }
	
	void NextScene()
    {
        SceneManager.LoadScene("02Level");
    }
}
