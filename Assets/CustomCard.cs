using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(cardName);
        player.pickCustomJokerCard(cardName);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // abc 
    }
}