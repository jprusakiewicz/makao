using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

public class ClicableCard : MonoBehaviour
{
    private bool isActve;
    private Player player;

    public bool IsActive()
    {
        return isActve;
    }

    private float move_distance;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(CardClick);
        player = GetComponentInParent<Player>();
        float move_percent = 5;
        var size_y = gameObject.GetComponent<UnityEngine.UI.Image>().sprite.bounds.size.y;
        move_distance = move_percent * size_y / 100;
    }

//    private void OnMouseDown()
//    {
//        CardClick();
//    }

    private void CardClick()
    {
        if (isActve)
        {
            isActve = false;
            moveDown();
            player.disableButtons();
        }
        else
        {
            if (!HandleFunctionCard(gameObject.GetComponent<UnityEngine.UI.Image>().sprite.name))
            {
                player.sendUpdate(gameObject.GetComponent<UnityEngine.UI.Image>().sprite.name);
                isActve = true;
                moveUp();
            }
        }
    }

    private static bool IsFunctionalWithCall(string n)
    {
        bool is_f = n.Last() == '1' || n.Last() == 'B'|| n.Last() == 'F';
        return is_f;
    }

    public bool HandleFunctionCard(string pickedCard)
    {
        if (!IsFunctionalWithCall(pickedCard)) return false;
        Debug.Log("open color call window");
        if (pickedCard.Last() == '1') //ace
            player.setColorCardsButtons(pickedCard);
        else if (pickedCard.Last() == 'B') //jack
            player.setFigureCardsButtons(pickedCard);
        else if (pickedCard.Last() == 'F') //joker
            player.setJokerScroll(pickedCard);

        moveUp();
        isActve = true;
        return true;
    }

    private void moveDown()
    {
        var new_position = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y - move_distance,
            gameObject.transform.position.z);
        gameObject.transform.position = new_position;
    }

    private void moveUp()
    {
        var new_position = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y + move_distance,
            gameObject.transform.position.z);
        gameObject.transform.position = new_position;
    }

    public void HideIfActive()
    {
        if (isActve)
            gameObject.SetActive(false);
    }

    public CardState GetState()
    {
        var state = new CardState(isActve, gameObject.GetComponent<SpriteRenderer>().sprite.name);
        return state;
    }

    public struct CardState
    {
        public CardState(bool _isActive, string _name)
        {
            isActive = _isActive;
            name = _name;
        }

        private bool isActive;
        private string name;
        public bool IsActive => isActive;
        public string Name => name;
    }
}