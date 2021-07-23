using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetCards : MonoBehaviour
{
    private SpriteCollection spriteCollection;

    [SerializeField] private GameObject pile;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject player;

    Pile pile_script;
    Player player_script;

    Enemy enemy_script1;
    Enemy enemy_script2;
    Enemy enemy_script3;

    void setCards()
    {
        List<GameObject> n = new List<GameObject>()
            {spriteCollection.GetCard("cardsBack"), spriteCollection.GetCard("cardsBack")};

        pile_script.Set(n);

//        player_script.Set();
        enemy_script1.setCards(4, spriteCollection.GetCard("cardsBack"));
        enemy_script2.setCards(5, spriteCollection.GetCard("cardsBack"));
        enemy_script3.setCards(6, spriteCollection.GetCard("cardsBack"));
    }

    private void Start()
    {
        spriteCollection = GetComponent<SpriteCollection>();

        pile_script = pile.GetComponent<Pile>();
        enemy_script1 = enemy1.GetComponent<Enemy>();
        enemy_script2 = enemy2.GetComponent<Enemy>();
        enemy_script3 = enemy3.GetComponent<Enemy>();

        setCards();
    }
}