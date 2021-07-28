using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetCards : MonoBehaviour
{
    private SpriteCollection _spriteCollection;

    [SerializeField] private GameObject pile;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject player;

    Pile _pileScript;
    Player _playerScript;

    Enemy _enemyScript1;
    Enemy _enemyScript2;
    Enemy _enemyScript3;

    void setCards()
    {
        var n = new List<Sprite>()
        {
            _spriteCollection.GetCardsSprite("Club05"),
            _spriteCollection.GetCardsSprite("Club02"),
        };

        _pileScript.Set(n);
        n.Add(_spriteCollection.GetCardsSprite("Club12"));
        n.Add(_spriteCollection.GetCardsSprite("Diamond09"));
        _playerScript.SetCards(_spriteCollection.GetCardsFront(), n);
        _enemyScript1.setCards(14, _spriteCollection.GetCardsBack());
        _enemyScript2.setCards(15, _spriteCollection.GetCardsBack());
        _enemyScript3.setCards(16, _spriteCollection.GetCardsBack());
    }

    private void Start()
    {
        _spriteCollection = GetComponent<SpriteCollection>();
        _playerScript = player.GetComponent<Player>();

        _pileScript = pile.GetComponent<Pile>();
        _enemyScript1 = enemy1.GetComponent<Enemy>();
        _enemyScript2 = enemy2.GetComponent<Enemy>();
        _enemyScript3 = enemy3.GetComponent<Enemy>();

        setCards();
    }

}