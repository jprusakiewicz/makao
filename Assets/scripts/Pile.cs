using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Pile : MonoBehaviour
{
    [SerializeField] private GameObject center_card_1;
    [SerializeField] private GameObject center_card_2;
    void Start()
    {
        string a = "\u1F0A1";
        string b = "\u1F0DF";
    }

    public void Set(List<GameObject> cards)
    {
        if (cards.Count > 0)
        {
            center_card_1.GetComponent<SpriteRenderer>().sprite = cards[0].GetComponent<SpriteRenderer>().sprite;
            if (cards.Count > 1)
            {
                center_card_2.SetActive(true);
                center_card_2.GetComponent<SpriteRenderer>().sprite = cards[1].GetComponent<SpriteRenderer>().sprite;
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
}