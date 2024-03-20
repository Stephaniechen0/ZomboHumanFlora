using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    public class Player
    {
        public int points;
    }

    int zombie = 1;
    int human = 2;
    int plant = 3;

    int p1Input;
    int p2Input;

    int p1Points;
    int p2Points;

    bool winnerCheck;

    int rounds;

    public TMP_Text p1scoreText;
    public TMP_Text p2scoreText;
    public TMP_Text roundNumText;
    public TMP_Text resultText;

    public AudioSource cheering;
    public AudioSource hmmm;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        p1scoreText.text = "PLAYER ONE'S \nSCORE: 0";
        p2scoreText.text = "PLAYER TWO'S \nSCORE: 0";

        rounds = 1; 
        roundNumText.text = "ROUND 1";
    }

    public void PickCard(int card, int playerID)
    {
        if (playerID == 0)
        {
            p1Input = card;
            Debug.Log("Player " + playerID + " chooses " + card);
        }

        if (playerID == 1)
        {
            p2Input = card;
            Debug.Log("Player " + playerID + " chooses " + card);
        }

        if (p1Input != 0 && p2Input != 0)
        {
            startRound();
        }
    }

    void startRound()
    {
        roundNumText.text = "ROUND " + rounds; 
        
        if (winnerCheck)
        {
            return;
        }

        // draw
        if (p1Input == p2Input)
        {
            hmmm.Play();
            resultText.text = "DRAW";
            PlayerNetwork.instance.updateResClientRPC(resultText.text);
            StartCoroutine(textShow());
            Debug.Log("draw");

            resetPick();
            return;
        }

        // zombie beats human
        if (p1Input == zombie && p2Input == human)
        {
            p1Points++;
            cheering.Play();

            resultText.text = "PLAYER ONE \nWINS";
            PlayerNetwork.instance.updateResClientRPC(resultText.text);
            StartCoroutine(textShow());
            p1scoreText.text = "PLAYER ONE'S \nSCORE: " + p1Points;

            if (p1Points == 3)
            {
                // player 1 wins
                winnerCheck = true;
                Debug.Log("p1 wins.");
                return;
            }

            rounds++;
            resetPick();
        }
        else if (p2Input == zombie && p1Input == human)
        {
            p2Points++;
            cheering.Play();

            resultText.text = "PLAYER TWO \nWINS";
            PlayerNetwork.instance.updateResClientRPC(resultText.text); 
            StartCoroutine(textShow());
            p2scoreText.text = "PLAYER TWO'S \nSCORE: " + p2Points;

            if (p2Points == 3)
            {
                // player 2 wins
                winnerCheck = true;
                Debug.Log("p2 wins.");
                return;
            }

            rounds++;
            resetPick();
        }

        // plant beats zombie
        if (p1Input == plant && p2Input == zombie)
        {
            p1Points++;
            cheering.Play();

            resultText.text = "PLAYER ONE \nWINS";
            PlayerNetwork.instance.updateResClientRPC(resultText.text); 
            StartCoroutine(textShow());
            p1scoreText.text = "PLAYER ONE'S \nSCORE: " + p1Points;

            if (p1Points == 3)
            {
                // player 1 wins
                winnerCheck = true;
                Debug.Log("p1 wins.");
                return;
            }

            rounds++;
            resetPick();
        }
        else if (p2Input == plant && p1Input == zombie)
        {
            p2Points++;
            cheering.Play();

            resultText.text = "PLAYER TWO \nWINS";
            PlayerNetwork.instance.updateResClientRPC(resultText.text); 
            StartCoroutine(textShow());
            p2scoreText.text = "PLAYER TWO'S \nSCORE: " + p2Points;

            if (p2Points == 3)
            {
                // player 2 wins
                winnerCheck = true;
                Debug.Log("p2 wins.");
                return;
            }

            rounds++;
            resetPick();
        }

        // human beats plant
        if (p1Input == human && p2Input == plant)
        {
            p1Points++;
            cheering.Play();

            resultText.text = "PLAYER ONE \nWINS";
            PlayerNetwork.instance.updateResClientRPC(resultText.text); 
            StartCoroutine(textShow());
            p1scoreText.text = "PLAYER ONE'S \nSCORE: " + p1Points;

            if (p1Points == 3)
            {
                // player 1 wins
                winnerCheck = true;
                Debug.Log("p1 wins.");
                return;
            }

            rounds++;
            resetPick();
        }
        else if (p2Input == human && p1Input == plant)
        {
            p2Points++;
            cheering.Play();

            resultText.text = "PLAYER TWO \nWINS";
            PlayerNetwork.instance.updateResClientRPC(resultText.text); 
            StartCoroutine(textShow());
            p2scoreText.text = "PLAYER TWO'S \nSCORE: " + p2Points;

            if (p2Points == 3)
            {
                // player 2 wins
                winnerCheck = true;
                Debug.Log("p2 wins.");
                return;
            }

            rounds++;
            resetPick();
        }
        return;
    }

    void resetPick()
    {
        p1Input = 0;
        p2Input = 0;
        PlayerNetwork.instance.updateP0ServerRPC(0);
        PlayerNetwork.instance.updateP1ServerRPC(0);
        PlayerNetwork.instance.updateScoresClientRPC(rounds, p1Points, p2Points);
    }

    IEnumerator textShow()
    {
        yield return new WaitForSeconds(2);
        resultText.text = "";
    }
}
