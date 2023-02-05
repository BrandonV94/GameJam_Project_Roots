using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public GameObject player1Ref;
    public GameObject player2Ref;

    public GameObject winnerText; 
    
    public bool gameOver = false;

    public GameObject gameOverScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (gameOver == false)
        {
            if (player1Ref && player1Ref.GetComponent<PlayerStats>().health <= 0)
            {
                Debug.Log("P2 Win");
                gameOver = true;
                winnerText.SetActive(true);
                winnerText.GetComponent<TMP_Text>().text = "Player 2 Wins!";
            }

            if (player2Ref && player2Ref.GetComponent<PlayerStats>().health <= 0)
            {
                Debug.Log("P1 Win");
                gameOver = true;
                winnerText.SetActive(true);
                winnerText.GetComponent<TMP_Text>().text = "Player 1 Wins!";
            }
        }
        else
        {
            gameOverScreen.SetActive(true);
        }
    }
}
