using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerInput : NetworkBehaviour
{
    public int card;
    public AudioSource flipNoise;

    public void PressButton()
    {
        Debug.Log("ID " + NetworkManager.Singleton.LocalClientId + " chooses " + card);
        flipNoise.Play(); 
        
        if (NetworkManager.Singleton.LocalClientId == 0)
        {
            PlayerNetwork.instance.updateP0ServerRPC(card);
        }
        else if (NetworkManager.Singleton.LocalClientId == 1)
        {
            PlayerNetwork.instance.updateP1ServerRPC(card);
        }
    }
}
