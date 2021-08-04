using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    private ConnectionManager connectionManager;

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
                i);
            var newCard = Instantiate(card, position, gameObject.transform.rotation);
            newCard.transform.SetParent(gameObject.transform);
            newCard.GetComponent<SpriteRenderer>().sprite = fronts[i];
        }
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

//    private bool IsAnyPlayerCardActive()
//    {
//        var playerCards = gameObject.GetComponentsInChildren<ClicableCard>();
//        foreach (var playerCard in playerCards)
//        {
//            ClicableCard.CardState state = playerCard.GetState();
//            if (state.IsActive)
//                return true;
//        }
//
//        return false;
//    }
//    private List<ClicableCard.CardState> GetPlayerCardsState()
//    {
//        var cardsStates = new List<ClicableCard.CardState>();
//        var playerCards = gameObject.GetComponentsInChildren<ClicableCard>();
//        foreach (var playerCard in playerCards)
//        {
//            cardsStates.Add(playerCard.GetState());
//        }
//
//        return cardsStates;
//    }
    
    public void RemoveCards()
    {
        foreach (Transform child in transform) {
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
        connectionManager.SendUpdateToServer(l);
    }
}
