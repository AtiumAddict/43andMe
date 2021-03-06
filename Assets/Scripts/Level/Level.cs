﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // UI panels
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject fadePanel;
    public Text gameOverText;

    AudioManager audioManager;
    public bool playing;
    public Image playerImage;

    public int[,] matrix = new int[1000, 3];    // The matrix that will hold all the nubmers
    // Subtraction and addition boxes
    public Text playerText, minus0, minus1, minus2, plus0, plus1, plus2;
    private Text[] allBoxes;
    private Text[] minusBoxes;
    private Text[] plusBoxes;

    public RectTransform pos0, pos1, pos2;  // The 3 possible player number positions
    protected int playerNo;
    public int startingNo;
    public int playerPos;   // A number that indicates the player number position
    private int currentRow; // A number that indicates on what row the player number is at the moment.

    /* The counters that show how many copies of the same number are behind the current oneon the 3 rows. 
     * For example, when the the counter2 is at 0 and the minus number on the third column is 27, there are
     * 2 more 27s behind it. When the counter2 is at 1, there is 1 more 27 and when it is at 2, the next 
     * number will be randomly generated. */
    private int counter0, counter1, counter2;
    public GameObject counter01, counter11, counter12, counter21, counter22, counter23;

    // Timer
    public float timer = 43f;
    public Text timerText;
    public GameObject timerObject;
    private Animator timerAnim;

    void Start ()
    {
        audioManager = AudioManager.instance;
        pauseMenu.SetActive(false);
        playing = true;
        //allBoxes = new Text[] { player, minus0, minus1, minus2, plus0, plus1, plus2 };
        minusBoxes = new Text[] { minus0, minus1, minus2 };
        plusBoxes = new Text[] { plus0, plus1, plus2 };
        startingNo = 43;
        currentRow = 0;
        counter0 = 0;
        counter1 = 0;
        counter2 = 0;
        playerNo = startingNo;
        playerText.text = playerNo.ToString();
        playerImage.rectTransform.anchoredPosition = pos1.anchoredPosition;
        playerPos = 1;
        timer = 43f;
        timerAnim = timerObject.GetComponent<Animator>();

        // Generate the minus numbers
        for (int i = 0; i < 3; i++)
        {
            AssignNumber(minusBoxes[i], GenerateNumber(), 1, i);
        }

        // Generate the plus numbers
        for (int i = 0; i < 3; i++)
        {
            AssignNumber(plusBoxes[i], 0, 0, i);
        }
    }

    void Update()
    {
        timerAnim.SetBool("isPlaying", playing);

        if (playing)
        {
            timer -= Time.deltaTime;
            timerText.text = (Mathf.RoundToInt(timer)).ToString();

            // If the timer reaches 0, the game is over.
            if (timer <= 0f)
            {
                GameOver("You're stuck with " + playerNo);
            }

            // Movement to the right
            if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
            {
                if (playerPos == 2)
                {
                    HorizontalMovement(0);
                }
                else
                {
                    HorizontalMovement(playerPos + 1);
                }
            }
            
            // Movement to the left
            if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
            {
                if (playerPos == 0)
                {
                    HorizontalMovement(2);
                }
                else
                {
                    HorizontalMovement(playerPos - 1);
                }
            }

            // Forward movement (subtraction)
            if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
            {
                ForwardMovement();
            }

            // Forward movement (addition)
            if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
            {
                BackwardMovement();
            }

            // Handle the counter line indicators:

            if (counter1 == 0 && !counter11.activeSelf)
            {
                counter11.SetActive(true);
            }

            else if (counter1 == 1 && counter11.activeSelf)
            {
                counter11.SetActive(false);
            }

            if (counter2 == 1 && !counter21.activeSelf)
            {
                counter21.SetActive(true);
                if (counter22.activeSelf)
                {
                    counter22.SetActive(false);
                }
            }

            else if (counter2 == 0 && !counter22.activeSelf)
            {
                counter22.SetActive(true);
                if (counter21.activeSelf)
                {
                    counter21.SetActive(false);
                }
            }

            else if (counter2 == 2 && (counter21.activeSelf || counter22.activeSelf))
            {
                counter21.SetActive(false);
                counter22.SetActive(false);
            }
        }

        else
        {
            if (Input.GetKeyDown("return") || Input.GetKeyDown("space") || Input.GetKeyDown("enter"))
            {
                if (gameOverMenu.activeSelf || winMenu.activeSelf)
                {
                    RestartLevel();
                }
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            TogglePauseMenu();
        }
    }

    // Assign the given number to the given text box, and, if the number isn't in the matrix, put it in the appropriate column and row.
    public void AssignNumber(Text txt, int num, int relativeRow, int col)
    {
        // Put the number in the text box.
        if (num == 0)
        {
            txt.text = "";
        }
        else
        {
            txt.text = num.ToString();
        }

        // If the matrix position empty, put the number there.
        if (matrix[currentRow + relativeRow, col] == 0)
        {
            matrix[currentRow + relativeRow, col] = num;
        }
    }

    // Horizontal change of position
    public void HorizontalMovement (int i)
    {
        switch (i)
        {
            case 0:
                playerImage.rectTransform.anchoredPosition = pos0.anchoredPosition;
                playerPos = 0;
                break;
            case 1:
                playerImage.rectTransform.anchoredPosition = pos1.anchoredPosition;
                playerPos = 1;
                break;
            case 2:
                playerImage.rectTransform.anchoredPosition = pos2.anchoredPosition;
                playerPos = 2;
                break;
            default:
                playerImage.rectTransform.anchoredPosition = pos1.anchoredPosition;
                playerPos = 1;
                break;
        }
    }

    // Generate random number
    public int GenerateNumber ()
    {
        return Random.Range(1, startingNo - 1);
    }

    // What happens when you move forward
    public void ForwardMovement()
    {
        // Subtract the number above the player from the player number.
        playerNo = playerNo - matrix[currentRow + 1, playerPos];
        playerText.text = playerNo.ToString();
        if (playerNo == 0)
        {
            audioManager.PlaySound("Win");
            winMenu.SetActive(true);
            playing = false;
            return;
        }

        // If the player number goes below 0, the game is over.
        else if (playerNo < 0)
        {
            GameOver("You can't go below 0!");
            return;
        }

        else
        {
            audioManager.PlaySound("Subtraction");
            UpdateNumbersForward();

            // Update current row.
            currentRow++;
        }

    }

    public void UpdateNumbersForward ()
    {
        // Update plus row
        for (int i = 0; i < 3; i++)
        {
            AssignNumber(plusBoxes[i], matrix[currentRow + 1, i], 1, i);
        }

        // Update 1st minus box
        if (matrix[currentRow + 2, 0] == 0)
        {
            if (counter0 < 0)
            {
                AssignNumber(minusBoxes[0], matrix[currentRow + 1, 0], 2, 0);
                counter0++;
            }
            else
            {
                AssignNumber(minusBoxes[0], GenerateNumber(), 2, 0);
                counter0 = 0;
            }
        }
        else
        {
            AssignNumber(minusBoxes[0], matrix[currentRow + 2, 0], 2, 0);

            if (counter0 < 0)
            {
                counter0++;
            }

            else
            {
                counter0 = 0;
            }
        }

        // Update 2nd minus box
        if (matrix[currentRow + 2, 1] == 0)
        {
            if (counter1 < 1)
            {
                AssignNumber(minusBoxes[1], matrix[currentRow + 1, 1], 2, 1);
                counter1++;
            }
            else
            {
                AssignNumber(minusBoxes[1], GenerateNumber(), 2, 1);
                counter1 = 0;
            }
        }
        else
        {
            AssignNumber(minusBoxes[1], matrix[currentRow + 2, 1], 2, 1);

            if (counter1 < 1)
            {
                counter1++;
            }

            else
            {
                counter1 = 0;
            }
        }

        // Update 3rd minus box
        if (matrix[currentRow + 2, 2] == 0)
        {
            if (counter2 < 2)
            {
                AssignNumber(minusBoxes[2], matrix[currentRow + 1, 2], 2, 2);
                counter2++;
            }
            else
            {
                AssignNumber(minusBoxes[2], GenerateNumber(), 2, 2);
                counter2 = 0;
            }
        }
        else
        {
            AssignNumber(minusBoxes[2], matrix[currentRow + 2, 2], 2, 2);

            if (counter2 < 2)
            {
                counter2++;
            }

            else
            {
                counter2 = 0;
            }
        }
    }

    // What happens when you move backwards
    public void BackwardMovement()
    {
        if (currentRow == 0)
        {
            // Tell the player that he can't go further back.
        }

        else
        {
            // Add the number below the player to the player number.
            playerNo = playerNo + matrix[currentRow, playerPos];
            playerText.text = playerNo.ToString();

            // If the player number goes over 43, the game is over.
            if (playerNo > 43)
            {
                GameOver("You can't go over 43!");
                return;
            }

            else
            {
                audioManager.PlaySound("Addition");
                UpdateNumbersBackward();
                // Update current row.
                currentRow--;
            }
        }
    }

    public void UpdateNumbersBackward()
    {
        // Update minus row
        for (int i = 0; i < 3; i++)
        {
            AssignNumber(minusBoxes[i], matrix[currentRow, i], 0, i);
            AssignNumber(plusBoxes[i], matrix[currentRow - 1, i], 0, i);
        }

        if (counter0 == 0)
        {
            counter0 = 0;
        }

        else
        {
            counter0 = 0;
        }

        if (counter1 == 0)
        {
            counter1 = 1;
        }

        else
        {
            counter1--;
        }

        if (counter2 == 0)
        {
            counter2 = 2;
        }

        else
        {
            counter2--;
        }
    }

    public void RestartLevel()
    {
        Scene currentLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentLevel.buildIndex);
    }

    // The game over screen is activated with the given message
    public void GameOver (string message)
    {
        audioManager.PlaySound("Over");
        gameOverText.text = message;
        gameOverMenu.SetActive(true);
        playing = false;
        Cursor.visible = true;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            playing = false;
        }

        else
        {
            playing = true;
        }
    }
}

