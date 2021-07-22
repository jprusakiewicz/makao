using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Pile : MonoBehaviour
{
    [SerializeField] private GameObject center_card_1;
    [SerializeField] private GameObject center_card_2;
    [SerializeField] private GameObject center_card_3;
//    private Sprite firstSprite;
//    private Sprite secondSprite;

    void Start()
    {
        center_card_3.SetActive(false);

        string a = "\u1F0A1";
        string b = "\u1F0DF";

//        firstSprite = center_card_1.GetComponent<SpriteRenderer>().sprite;
//        secondSprite = center_card_1.GetComponent<SpriteRenderer>().sprite;

    }

    public void Set(List<Sprite> cards)
    {
        if (cards.Count > 0)
        {
            center_card_1.GetComponent<SpriteRenderer>().sprite = cards[0];
            if (cards.Count > 1)
            {
                center_card_2.SetActive(true);
                center_card_2.GetComponent<SpriteRenderer>().sprite = cards[1];
            }
            else
            {
                center_card_2.SetActive(false);
            }
        }
        else
        {
            center_card_1.SetActive(false);
        }
    }
    public void SetThirdCard()
    {
        center_card_3.SetActive(true);

    }
}