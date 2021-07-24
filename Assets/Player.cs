using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void SetCards(int numberOfCards, GameObject card)
    {
        int offsetPercent = setOffsetPercent(numberOfCards);
        
        var cardWidth = card.GetComponent<SpriteRenderer>().bounds.size.x;
        
        var offset = numberOfCards * offsetPercent * cardWidth / 100; 
        var begining = gameObject.transform.localPosition.x - (offset / 2);
        for (int i = 0; i < numberOfCards; i++)
        {
            float newX = (begining + ((i + 1) * offsetPercent * cardWidth) / 100);

            Vector3 position = new Vector3(newX,
                gameObject.transform.position.y,
                i);
            var newCard = Instantiate(card, position, gameObject.transform.rotation);
            newCard.transform.SetParent(gameObject.transform);
        }
    }

    private static int setOffsetPercent(int numberOfCards)
    {
        int offsetPercent;
        if (numberOfCards > 17)
            offsetPercent = 43;
        else if (numberOfCards > 13)
            offsetPercent = 55;
        else if (numberOfCards > 9)
            offsetPercent = 59;
        else if (numberOfCards > 7)
            offsetPercent = 85;
        else if (numberOfCards > 5)
            offsetPercent = 92;
        else
            offsetPercent = 94;

        return offsetPercent;
    }
}
