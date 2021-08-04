using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        player = GetComponentInParent<Player>();   
        float move_percent = 10;
        var size_y = gameObject.GetComponent<SpriteRenderer>().size.y;
        move_distance = move_percent * size_y / 100;
    }

    private void OnMouseDown()
    {
        CardClick();
    }

    private void CardClick()
    {
        if (isActve)
        {
            isActve = false;
            moveUp();
        }
        else
        {
            player.sendUpdate(gameObject.GetComponent<SpriteRenderer>().sprite.name);
            isActve = true;
            moveDown();
        }
    }

    private void moveUp()
    {
        var new_position = new Vector3(gameObject.transform.position.x,
                                    gameObject.transform.position.y - move_distance,
                                       gameObject.transform.position.z);
        gameObject.transform.position = new_position;
    }

    private void moveDown()
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