using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClicableCard : MonoBehaviour
{
    private void OnMouseDown()
    {
        var a = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        Debug.Log (a);
        gameObject.SetActive(false);


    }
}
