using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public AudioManager audioManager;

    void Awake ()
    {
        if (Instance == null)
        {
            instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        audioManager = AudioManager.instance;
        
    }
	
    void Update ()
    {
        
    }
}
