using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCall : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameObject.Find("player").GetComponent<Player>();
    }

    public void call_spades()
    {
        Debug.Log("calling spades");
        player.callColor("Spades");
    }

    public void call_club()
    {
        Debug.Log("calling clubs");
        player.callColor("Clubs");

    }

    public void call_heart()
    {
        Debug.Log("calling hearts");
        player.callColor("Hearts");

    }

    public void call_diamond()
    {
        Debug.Log("calling diamonds");
        player.callColor("Diamonds");

    }
}