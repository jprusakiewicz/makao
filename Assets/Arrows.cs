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


        if (arrowName == "player")
            playerArrow.SetActive(true);
        else if (arrowName == "left")
            leftEnemyArrow.SetActive(true);
        else if (arrowName == "top")
            topEnemyArrow.SetActive(true);
        else if (arrowName == "right")
            rightEnemyArrow.SetActive(true);
    }
}