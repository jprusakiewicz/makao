using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    private ConnectionManager connectionManager;
    [SerializeField] private GameObject colorCall;
    [SerializeField] private GameObject figureCall;
    [SerializeField] private GameObject jokerScroll;
    private string pickedCard;


    private void Start()
    {
        connectionManager = GameObject.Find("GameController").GetComponent<ConnectionManager>();
    }

    public void SetCards(GameObject card, List<Sprite> fronts)
    {
        int numberOfCards = fronts.Count;
        int offsetPercent = SetOffsetPercent(numberOfCards);
        var cardWidth = card.GetComponent<SpriteRenderer>().bounds.size.x;
        var offset = numberOfCards * offsetPercent * cardWidth / 100;
        var begining = gameObject.transform.localPosition.x - (offset / 2);
        for (int i = 0; i < numberOfCards; i++)
        {
            float newX = (begining + ((i + 1) * offsetPercent * cardWidth) / 100);
            Vector3 position = new Vector3(newX,
                gameObject.transform.position.y,
                50-i);
            var newCard = Instantiate(card, position, gameObject.transform.rotation);
            newCard.transform.SetParent(gameObject.transform);
            newCard.GetComponent<SpriteRenderer>().sprite = fronts[i];
        }
        //reset
        disableButtons();
    }

    private static int SetOffsetPercent(int numberOfCards)
    {
        int offsetPercent;
        if (numberOfCards > 17)
            offsetPercent = 43;
        else if (numberOfCards > 13)
            offsetPercent = 55;
        else if (numberOfCards > 9)
            offsetPercent = 59;
        else if (numberOfCards > 7)
            offsetPercent = 85;
        else if (numberOfCards > 5)
            offsetPercent = 92;
        else
            offsetPercent = 94;

        return offsetPercent;
    }

    public void RemoveCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void HideActiveCards()
    {
        var playerCards = gameObject.GetComponentsInChildren<ClicableCard>();
        foreach (var playerCard in playerCards)
        {
            playerCard.HideIfActive();
        }
    }

    public void sendUpdate(string pickedCard)
    {
        var l = new List<string> {pickedCard};
        ConnectionManager.SendUpdateToServer(l);
    }

    public void setColorCardsButtons(string pickedCard)
    {
        this.pickedCard = pickedCard;
        colorCall.SetActive(true);
    }
    public void setFigureCardsButtons(string pickedCard)
    {
        this.pickedCard = pickedCard;
        figureCall.SetActive(true);
    }

    public void disableButtons()
    {
        colorCall.SetActive(false);
        figureCall.SetActive(false);
        jokerScroll.SetActive(false);

    }

    public void callColor(string color)
    {
        connectionManager.CallColor(color, pickedCard);
        pickedCard = null;
    }

    public void callFigure(string figure)
    {
        ConnectionManager.CallFigure(figure, pickedCard);
        pickedCard = null;
    }

    public void setJokerScroll(string jokerCard)
    {
        pickedCard = jokerCard;
        jokerScroll.SetActive(true);
    }

    public void pickCustomJokerCard(string customCard)
    {
        ConnectionManager.PickCustomJokerCard(customCard, pickedCard);
        pickedCard = null;
    }
}