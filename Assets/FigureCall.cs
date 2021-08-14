using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCall : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Player>();

    }

    public void call_five()
    {
        Debug.Log("calling spades");
        player.callFigure("Five");
    }
    public void call_six()
    {
        Debug.Log("calling spades");
        player.callFigure("Six");
    }
    public void call_seven()
    {
        Debug.Log("calling spades");
        player.callFigure("Seven");
    }
    public void call_eight()
    {
        Debug.Log("calling spades");
        player.callFigure("Eight");
    }
    public void call_nine()
    {
        Debug.Log("calling spades");
        player.callFigure("Nine");
    }
    public void call_ten()
    {
        Debug.Log("calling spades");
        player.callFigure("Ten");
    }
}
