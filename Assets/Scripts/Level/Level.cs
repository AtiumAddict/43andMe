using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

    public Text player, txt4, txt5, txt6, txt7, txt8, tx9;
    public RectTransform pos1, pos2, pos3;
    private int playerNo;
    public int startingNo;
    public int playerPos;
    public int counter2, counter3;

    void Start ()
    {
        startingNo = 43;
        playerNo = startingNo;
        player.text = playerNo.ToString();
        player.rectTransform.anchoredPosition = pos2.anchoredPosition;
        playerPos = 2;
    }
	
	void Update ()
    {
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            if (playerPos == 3)
            {
                SwitchPosition(1);
            }
            else
            {
                SwitchPosition(playerPos + 1);
            }
        }
    }

    public void SwitchPosition (int i)
    {
        switch (i)
        {
            case 1:
                player.rectTransform.anchoredPosition = pos1.anchoredPosition;
                playerPos = 1;
                break;
            case 2:
                player.rectTransform.anchoredPosition = pos2.anchoredPosition;
                playerPos = 2;
                break;
            case 3:
                player.rectTransform.anchoredPosition = pos3.anchoredPosition;
                playerPos = 3;
                break;
            default:
                player.rectTransform.anchoredPosition = pos2.anchoredPosition;
                playerPos = 2;
                break;
        }
    }

    public int GenerateNumber ()
    {
        return Random.Range(0, playerNo);
    }
}

