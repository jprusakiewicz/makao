using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int offset_percent = 30;


    public void setCards(int numberOfCards, GameObject card)
    {
        var card_width = card.GetComponent<SpriteRenderer>().bounds.size.x;

        if (numberOfCards > 6)
            offset_percent = 20;
        if (numberOfCards > 8)
            offset_percent = 15;

        var offset = numberOfCards * offset_percent * card_width / 100; 
        if (math.abs(gameObject.transform.eulerAngles.z) == 90)
        {
            var begining = gameObject.transform.localPosition.y - (offset / 2);
            for (int i = 0; i < numberOfCards; i++)
            {
                float new_y = (begining + ((i + 1) * offset_percent * card_width) / 100);

                Vector3 position = new Vector3(
                    gameObject.transform.position.x,
                    new_y,
                    i);
                var newCard = Instantiate(card, position, gameObject.transform.rotation);
                newCard.transform.parent = gameObject.transform;
            }
        }
        else if (math.abs(gameObject.transform.eulerAngles.z) == 270)
        {
            var begining = gameObject.transform.localPosition.y + (offset / 2);
            for (int i = 0; i < numberOfCards; i++)
            {
                int j = numberOfCards - i;
                float new_y = (begining - ((i + 1) * offset_percent * card_width) / 100);

                Vector3 position = new Vector3(
                    gameObject.transform.position.x,
                    new_y,
                    j);
                var newCard = Instantiate(card, position, gameObject.transform.rotation);
                newCard.transform.parent = gameObject.transform;
            }
        }
        else
        {
            var begining = gameObject.transform.localPosition.x - (offset / 2);
            for (int i = 0; i < numberOfCards; i++)
            {
                float new_x = (begining + ((i + 1) * offset_percent * card_width) / 100);

                Vector3 position = new Vector3(new_x,
                    gameObject.transform.position.y,
                    i);
                var newCard = Instantiate(card, position, gameObject.transform.rotation);
                newCard.transform.parent = gameObject.transform;
            }
        }
    }
}