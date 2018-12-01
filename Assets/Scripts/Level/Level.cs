using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text player, minus0, minus1, minus2, plus0, plus1, plus2;
    private Text[] allBoxes;
    private Text[] minusBoxes;
    private Text[] plusBoxes;
    public RectTransform pos0, pos1, pos2;
    private int playerNo;
    public int startingNo;
    public int playerPos;
    private int counter0, counter1, counter2;
    public int[,] matrix = new int [1000,3];
    private int currentRow;

    void Start ()
    {
        allBoxes = new Text[] { player, minus0, minus1, minus2, plus0, plus1, plus2 };
        minusBoxes = new Text[] { minus0, minus1, minus2 };
        plusBoxes = new Text[] { plus0, plus1, plus2 };
        startingNo = 43;
        currentRow = 0;
        counter0 = 0;
        counter1 = 0;
        counter2 = 0;
        playerNo = startingNo;
        player.text = playerNo.ToString();
        player.rectTransform.anchoredPosition = pos1.anchoredPosition;
        playerPos = 1;
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

        if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            ForwardMovement();
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("w"))
        {
            BackwardMovement();
        }
    }

    // Assign the given number to the given text box.
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
                player.rectTransform.anchoredPosition = pos0.anchoredPosition;
                playerPos = 0;
                break;
            case 1:
                player.rectTransform.anchoredPosition = pos1.anchoredPosition;
                playerPos = 1;
                break;
            case 2:
                player.rectTransform.anchoredPosition = pos2.anchoredPosition;
                playerPos = 2;
                break;
            default:
                player.rectTransform.anchoredPosition = pos1.anchoredPosition;
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
        player.text = playerNo.ToString();
        if (playerNo == 0)
        {

        }
        UpdateNumbersForward();

        // Update current row.
        currentRow++;
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
            player.text = playerNo.ToString();
            UpdateNumbersBackward();
            currentRow--;
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
    }
}

