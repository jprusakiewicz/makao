using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCollection : MonoBehaviour
{
    [SerializeField] private Sprite karta;


    public Sprite GetCard(string name)
    {
        return karta;
    }
}
