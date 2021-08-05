using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nicks : MonoBehaviour
{
    [SerializeField] private GameObject playerNick;
    [SerializeField] private GameObject leftNick;
    [SerializeField] private GameObject rightNick;
    [SerializeField] private GameObject topNick;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateNicks();
    }

    public void DeactivateNicks()
    {
        playerNick.SetActive(false);
        leftNick.SetActive(false);
        topNick.SetActive(false);
        rightNick.SetActive(false);
    }

    public void ActivateNicks(Dictionary<string, string> nicks)
    {
        DeactivateNicks();
        if (nicks.TryGetValue("player", out var player))
        {
            Debug.Log("n  "+ player);
            playerNick.GetComponent<TMPro.TextMeshProUGUI>().text = player;
            playerNick.SetActive(true);
        }

        if (nicks.TryGetValue("top", out var top))
        {
            Debug.Log("n  "+ top);

            topNick.GetComponent<TMPro.TextMeshProUGUI>().text = top;
            topNick.SetActive(true);

        }

        if (nicks.TryGetValue("left", out var left))
        {
            Debug.Log("n  "+ left);

            leftNick.GetComponent<TMPro.TextMeshProUGUI>().text = left;
            leftNick.SetActive(true);

        }

        if (nicks.TryGetValue("right", out var right))
        {
            Debug.Log("n  "+ right);

            rightNick.GetComponent<TMPro.TextMeshProUGUI>().text = right;
            rightNick.SetActive(true);

        }
    }
}