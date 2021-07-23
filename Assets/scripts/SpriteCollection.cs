using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCollection : MonoBehaviour
{
    [SerializeField] private GameObject cardsBack;


    public GameObject GetCard(string name)
    {
        if (name == "cardsBack")
            return cardsBack;
        else
        {
            return null;
        }
    }
}
