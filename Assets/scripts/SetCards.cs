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
        List<GameObject> n = new List<GameObject>()
            {_spriteCollection.GetEnemyCard(), _spriteCollection.GetEnemyCard()};

        _pileScript.Set(n);

        _playerScript.SetCards(5, _spriteCollection.GetPlayersCard("Club01"));
        _enemyScript1.setCards(14, _spriteCollection.GetEnemyCard());
        _enemyScript2.setCards(15, _spriteCollection.GetEnemyCard());
        _enemyScript3.setCards(16, _spriteCollection.GetEnemyCard());
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