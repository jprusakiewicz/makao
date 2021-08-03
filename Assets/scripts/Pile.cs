using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Pile : MonoBehaviour
{
    [SerializeField] private GameObject center_card_1;
    [SerializeField] private GameObject center_card_2;

    public void Set(List<Sprite> cards)
    {
        if (cards.Count > 0)
        {
            center_card_1.GetComponent<SpriteRenderer>().sprite = cards[0];
            center_card_1.SetActive(true);

            if (cards.Count > 1)
            {
                center_card_2.GetComponent<SpriteRenderer>().sprite = cards[1];
                center_card_2.SetActive(true);
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
    
    public void RemoveCards()
    {
        center_card_1.SetActive(false);
        center_card_2.SetActive(false);
    }
}