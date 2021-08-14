using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private GameObject playerArrow;
    [SerializeField] private GameObject leftEnemyArrow;
    [SerializeField] private GameObject topEnemyArrow;
    [SerializeField] private GameObject rightEnemyArrow;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateArrows();
    }

    public void DeactivateArrows()
    {
        playerArrow.SetActive(false);
        leftEnemyArrow.SetActive(false);
        topEnemyArrow.SetActive(false);
        rightEnemyArrow.SetActive(false);
    }

    public void ActivateArrow(string arrowName)
    {
        DeactivateArrows();
        
        switch (arrowName)
        {
            case "player":
                playerArrow.SetActive(true);
                break;
            case "left":
                leftEnemyArrow.SetActive(true);
                break;
            case "top":
                topEnemyArrow.SetActive(true);
                break;
            case "right":
                rightEnemyArrow.SetActive(true);
                break;
        }
    }
}