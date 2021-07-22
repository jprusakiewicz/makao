using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetCards : MonoBehaviour
{
    [SerializeField] private GameObject pile;
    private SpriteCollection spriteCollection;

    
    void setCards()
    {
        Pile pile_aaa = pile.GetComponent<Pile>();
        Sprite s = spriteCollection.GetCard("aaa");
        List<Sprite> sl = new List<Sprite>();
        sl.Append(s);
        pile_aaa.Set(sl);
    }

    private void Start()
    {
        spriteCollection = GetComponent<SpriteCollection>();
        setCards();
    }
}
