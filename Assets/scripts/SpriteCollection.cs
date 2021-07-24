using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class SpriteCollection : MonoBehaviour
{
    [SerializeField] private GameObject cardsBack;
    [SerializeField] private GameObject cardsFront;


    public GameObject GetPlayersCard(string cardName)
    {
        var cardToReturn = cardsFront;
        cardToReturn.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Path.Combine("Playing Cards",cardName));

        return cardToReturn;
    }

    public GameObject GetEnemyCard()
    {
        return cardsBack;
    }
}