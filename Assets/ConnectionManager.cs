using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using UnityEngine;
using BestHTTP;
using BestHTTP.Extensions;
using BestHTTP.WebSocket;
using Newtonsoft.Json;

public class Item
{
    public bool is_game_on;
    public string whos_turn;
    public CardsConfig game_data;
    public Dictionary<string, string> nicks;
}

public class Config
{
    public string player_id;
    public string room_id;
    public string server_address;
}

public class ConnectionManager : MonoBehaviour
{
    private static WebSocket webSocket;
    [SerializeField] private bool isMyTurn;
    [SerializeField] private Config config;
    private GameObject[] disableUIs;
    private SetCards setCards;
    private Arrows arrows;
    private Nicks nicks;


    private const float connectTimeout = 3;
    private float time_from_last_connection_request = connectTimeout;

    void Start()
    {
        config = new Config();
        setCards = GameObject.Find("SpriteCollection").GetComponent<SetCards>();
        arrows = GameObject.Find("arrows").GetComponent<Arrows>();
        nicks = GameObject.Find("nicks").GetComponent<Nicks>();

//        disableUIs = GameObject.FindGameObjectsWithTag("DisableUI");
//        foreach (GameObject go in disableUIs)
//        {
//            go.SetActive(false);
//        }

        config = new Config {player_id = "1", room_id = "1", server_address = "ws://localhost:5000/ws/"}; // todo
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(config.player_id) && webSocket == null ||
            !string.IsNullOrEmpty(config.player_id) && !webSocket.IsOpen)
        {
            if (time_from_last_connection_request >= connectTimeout)
            {
                Debug.Log("Opening connection!");
                webSocket = ConnectToServer(config);

                time_from_last_connection_request = 0;
            }
            else
            {
                Debug.Log("No Connection!!!");
                ClearDesk();
                time_from_last_connection_request += Time.deltaTime;
            }
        }

//        else if (isMyTurn)
//        {
//            //try_send_turn(); todo send my state
//            Debug.Log("sending not implemented");
//        }
    }

    private void ClearDesk()
    {
        arrows.DeactivateArrows();
        nicks.DeactivateNicks();
        setCards.ResetCards();
    }

    private WebSocket ConnectToServer(Config config)
    {
        string full_address = Path.Combine(config.server_address + config.room_id + "/" + config.player_id);
        Debug.Log("full_path: " + full_address);

        webSocket = new WebSocket(new Uri(full_address));
        webSocket.OnMessage += OnMessageRecieved;
        webSocket.Open();

        return webSocket;
    }

    private void OnMessageRecieved(WebSocket webSocket, string message)
    {
        Debug.Log(message);
        Item item = JsonConvert.DeserializeObject<Item>(message);
        if (item.is_game_on)
        {
            setCards.setCards(item.game_data);
            arrows.ActivateArrow(item.whos_turn);
            nicks.ActivateNicks(item.nicks);
        }
    }

    public void SendUpdateToServer(List<string> cardName)
    {
        var dict_to_send = new Dictionary<string, List<string>>
        {
            ["picked_cards"] = cardName
        };

        string dict_as_str = JsonConvert.SerializeObject(dict_to_send);
        Debug.Log("sending update to server ");

        webSocket.Send(dict_as_str);
    }

    public void PickNewCard()
    {
        string string_to_send = "{\"other_move\": {\"type\": \"pick_new_card\"}}";
        Debug.Log("sending update to server ");

        webSocket.Send(string_to_send);
    }

    public void SkipMove()
    {
        string string_to_send = "{\"other_move\": {\"type\": \"skip\"}}";
        Debug.Log("sending update to server ");

        webSocket.Send(string_to_send);
    }

    public void ConfigFromJson(string json)
    {
        config = JsonUtility.FromJson<Config>(json);
    }

    public void ChangeIsMyTurnFalse()
    {
        config.player_id = null;
    }

    public bool IsMyTurn
    {
        get => isMyTurn;
        set => isMyTurn = value;
    }

    public void callColor(string colorCall, string cardName)
    {
        var l = new List<string> {cardName};
        var call = new Dictionary<string, Dictionary<string, string>>()
        {
            ["call"] = new Dictionary<string, string>
                {["color"] = colorCall}
        };
        var dict_to_send = new Dictionary<string, dynamic>
        {
            ["picked_cards"] = l,
            ["functional"] = call
        };

        string dict_as_str = JsonConvert.SerializeObject(dict_to_send);
        Debug.Log("sending update to server ");

        webSocket.Send(dict_as_str);
    }

    public void callFigure(string figureCall, string cardName)
    {
        var l = new List<string> {cardName};
        var call = new Dictionary<string, Dictionary<string, string>>()
        {
            ["call"] = new Dictionary<string, string>
                {["figure"] = figureCall}
        };
        var dict_to_send = new Dictionary<string, dynamic>
        {
            ["picked_cards"] = l,
            ["functional"] = call
        };

        string dict_as_str = JsonConvert.SerializeObject(dict_to_send);
        Debug.Log("sending update to server ");

        webSocket.Send(dict_as_str);
    }
}