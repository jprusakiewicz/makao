using System;
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

    public void SetAllCards(CardsConfig cardsConfig)
    {
        ResetCards();
        
        var pileCards = new List<Sprite>();
        foreach (var pileCard in cardsConfig.pile)
        {
            pileCards.Add(_spriteCollection.GetCardsSprite(pileCard));
        }
        
        var playerCards = new List<Sprite>();
        foreach (var playerCard in cardsConfig.player_hand)
        {            
            playerCards.Add(_spriteCollection.GetCardsSprite(playerCard));
        }

        _pileScript.Set(pileCards);

        _playerScript.SetCards(_spriteCollection.GetCardsFront(), playerCards);

        var cardsBack = _spriteCollection.GetCardsBack();
        if (cardsConfig.rest_players.TryGetValue("left", out var leftPlayer))
        {
            _enemyScript1.setCards(leftPlayer, cardsBack);
        }

        if (cardsConfig.rest_players.TryGetValue("top", out var topPlayer))
        {
            _enemyScript2.setCards(topPlayer, cardsBack);

        }

        if (cardsConfig.rest_players.TryGetValue("right", out var rightPlayer))
        {
            _enemyScript3.setCards(rightPlayer, cardsBack);

        }
    }

    public void ResetCards()
    {
        _playerScript.RemoveCards();
        _enemyScript1.RemoveCards();
        _enemyScript2.RemoveCards();
        _enemyScript3.RemoveCards();
        _pileScript.RemoveCards();
    }

    private void Start()
    {
        _spriteCollection = GetComponent<SpriteCollection>();
        _playerScript = player.GetComponent<Player>();

        _pileScript = pile.GetComponent<Pile>();
        _enemyScript1 = enemy1.GetComponent<Enemy>();
        _enemyScript2 = enemy2.GetComponent<Enemy>();
        _enemyScript3 = enemy3.GetComponent<Enemy>();
    }
}

public class CardsConfig
{
    // do not change variables names names
    public List<string> player_hand;
    public List<string> pile;
    public IDictionary<string,int> rest_players;
}

