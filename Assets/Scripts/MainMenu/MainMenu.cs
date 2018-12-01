using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start ()
    {
        Cursor.visible = true;
    }
    public void GoToLevel01()
    {
        SceneManager.LoadScene("02Level");
    }
}
