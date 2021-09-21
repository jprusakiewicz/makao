using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CustomCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player player;
    private string cardName;

    private void Start()
    {
        player = GameObject.Find("player").GetComponent<Player>();
        cardName = GetComponent<UnityEngine.UI.Image>().sprite.name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CustomCardClick();
    }

    private void CustomCardClick()
    {
        if (!HandleFunctionCard(cardName))
        {
            player.pickCustomJokerCard(cardName);

        }
    }
    public bool HandleFunctionCard(string pickedCard)
    {
        if (!IsFunctionalWithCall(pickedCard)) return false;
        Debug.Log("open color call window");
        if (pickedCard.Last() == '1') //ace
            player.setColorCardsButtons(pickedCard);
        else if (pickedCard.Last() == 'B') //jack
            player.setFigureCardsButtons(pickedCard);

        return true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // abc 
    }
    
    private static bool IsFunctionalWithCall(string n)
    {
        bool is_f = n.Last() == '1' || n.Last() == 'B'|| n.Last() == 'F';
        return is_f;
    }
}