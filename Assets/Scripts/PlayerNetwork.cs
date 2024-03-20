using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public static PlayerNetwork instance;
    //public AudioSource cheering;

    void Awake()
    {
        instance = this;
    }

    [ServerRpc(RequireOwnership = false)]
    public void updateP0ServerRPC(int card0)
    {
        GameManager.instance.PickCard(card0, 0);
    }

    [ServerRpc(RequireOwnership = false)]
    public void updateP1ServerRPC(int card1)
    {
        GameManager.instance.PickCard(card1, 1);
    }

    [ClientRpc]
    public void updateScoresClientRPC(int r, int p1, int p2)
    {
        GameManager.instance.roundNumText.text = "ROUND " + r;
        GameManager.instance.p1scoreText.text = "PLAYER ONE'S \nSCORE: " + p1;
        GameManager.instance.p2scoreText.text = "PLAYER TWO'S \nSCORE: " + p2;
    }

    [ClientRpc]
    public void updateResClientRPC(string str)
    {
        //cheering.Play();
        GameManager.instance.resultText.text = str;
        StartCoroutine(textShow());
    }

    IEnumerator textShow()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.resultText.text = "";
    }
}
