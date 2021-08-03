using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class SpriteCollection : MonoBehaviour
{
    [SerializeField] private GameObject cardsBack;
    [SerializeField] private GameObject cardsFront;


    public GameObject GetCardsFront()
    {
        return cardsFront;
    }

    public GameObject GetCardsBack()
    {
        return cardsBack;
    }
    
    public Sprite GetCardsSprite(string cardName)
    {    
        return Resources.Load<Sprite>(Path.Combine("Playing Cards", cardName));
    }
}