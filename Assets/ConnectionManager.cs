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
using UnityEngine.UIElements;

public class Item
{
    public bool is_game_on;
    public string whos_turn;
    public string call;
    public CardsConfig game_data;
    public Dictionary<string, string> nicks;
    public DateTime timestamp { get; set; }

}

public class Config
{
    // do not change variables names names
    public string player_id;
    public string room_id;
    public string server_address;
    public string player_nick;
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
    [SerializeField] private GameObject call;
    [SerializeField] private GameObject btns;
    [SerializeField] private GameObject waitingText;
    private Timer timer;



    private const float connectTimeout = 3;
    private float timeFromLastConnectionRequest = connectTimeout;

    void Start()
    {
        
        setCards = GameObject.Find("SpriteCollection").GetComponent<SetCards>();
        arrows = GameObject.Find("arrows").GetComponent<Arrows>();
        nicks = GameObject.Find("nicks").GetComponent<Nicks>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        waitingText.SetActive(true);
        btns.SetActive(false);

        

//        config = new Config
//            {player_id = "1", room_id = "1", server_address = "ws://localhost:5000/test/", player_nick = "komp"}; // todo
    }

    private void Update()
    {
        if ((string.IsNullOrEmpty(config.player_id) || webSocket != null) &&
            (string.IsNullOrEmpty(config.player_id) || webSocket.IsOpen)) return;
        if (timeFromLastConnectionRequest >= connectTimeout)
        {
            Debug.Log("Opening connection!");
            webSocket = ConnectToServer(config);
            timeFromLastConnectionRequest = 0;
        }
        else
        {
            Debug.Log("No Connection!!!");
            ClearDesk();
            timeFromLastConnectionRequest += Time.deltaTime;
        }
    }

    private void ClearDesk()
    {
        arrows.DeactivateArrows();
        nicks.DeactivateNicks();
        setCards.ResetCards();
        call.SetActive(false);
        btns.SetActive(false);
        waitingText.SetActive(false);
    }

    private WebSocket ConnectToServer(Config config)
    {
        string fullAddress = Path.Combine(config.server_address + config.room_id + "/" + config.player_id + "/" +
                                          config.player_nick);
        webSocket = new WebSocket(new Uri(fullAddress));
        webSocket.OnMessage += OnMessageRecieved;
        webSocket.Open();

        return webSocket;
    }

    private void OnMessageRecieved(WebSocket webSocket, string message)
    {
        ClearDesk();
        Item item = JsonConvert.DeserializeObject<Item>(message);
        if (item.is_game_on)
        {
            setCards.SetAllCards(item.game_data);
            arrows.ActivateArrow(item.whos_turn);
            nicks.ActivateNicks(item.nicks);
            waitingText.SetActive(false);
            call.SetActive(false);
            timer.SetTimer(item.timestamp);


            if (!string.IsNullOrEmpty(item.call))
            {
                call.SetActive(true);
                Debug.Log("loading call texture; " + item.call);
                call.GetComponent<UnityEngine.UI.Image>().sprite =
                    Resources.Load<Sprite>(Path.Combine("Call", item.call));
            }

            if (item.whos_turn == "player")
            {
                btns.SetActive(true);
            }
        }
        else
        {
            btns.SetActive(false);
            setCards.ResetCards();
            setCards.SetAllCards(item.game_data);
            nicks.ActivateNicks(item.nicks);
            waitingText.SetActive(true);
        }
    }

    public static void SendUpdateToServer(List<string> cardName)
    {
        var dictToSend = new Dictionary<string, List<string>>
        {
            ["picked_cards"] = cardName
        };
        
        string dictAsStr = JsonConvert.SerializeObject(dictToSend);
        webSocket.Send(dictAsStr);
    }

    public void PickNewCard()
    {
        string stringToSend = "{\"other_move\": {\"type\": \"pick_new_card\"}}";
        webSocket.Send(stringToSend);
    }

    public void SkipMove()
    {
        string stringToSend = "{\"other_move\": {\"type\": \"skip\"}}";
        webSocket.Send(stringToSend);
    }

    public void SendMakao()
    {
        string stringToSend = "{\"makao_move\": {\"type\": \"makao\"}}";
        webSocket.Send(stringToSend);
    }

    public void OnLeftNickClick()
    {
        string stringToSend = "{\"makao_move\": {\"type\": \"nick_click\",\"direction\": \"left\"}}";
        webSocket.Send(stringToSend);
    }

    public void OnRightNickClick()
    {
        string stringToSend = "{\"makao_move\": {\"type\": \"nick_click\",\"direction\": \"right\"}}";
        webSocket.Send(stringToSend);
    }

    public void OnTopNickClick()
    {
        string stringToSend = "{\"makao_move\": {\"type\": \"nick_click\",\"direction\": \"top\"}}";

        webSocket.Send(stringToSend);
    }

    public void ConfigFromJson(string json)
    {
        Debug.Log("config from json");
        if (config == null)
            config = JsonUtility.FromJson<Config>(json);
    }

    public void ChangeIsMyTurnFalse()
    {
        config.player_id = null;
    }

    public void CallColor(string colorCall, string cardName)
    {
        var call = new Dictionary<string, Dictionary<string, string>>()
        {
            ["call"] = new Dictionary<string, string>
                {["color"] = colorCall}
        };
        var dictToSend = new Dictionary<string, dynamic>
        {
            ["picked_cards"] = new List<string> {cardName},
            ["functional"] = call
        };

        webSocket.Send(JsonConvert.SerializeObject(dictToSend));
    }

    public static void CallFigure(string figureCall, string cardName)
    {
        var call = new Dictionary<string, Dictionary<string, string>>()
        {
            ["call"] = new Dictionary<string, string>
                {["figure"] = figureCall}
        };
        var dictToSend = new Dictionary<string, dynamic>
        {
            ["picked_cards"] = new List<string> {cardName},
            ["functional"] = call
        };

        webSocket.Send(JsonConvert.SerializeObject(dictToSend));
    }

    public static void PickCustomJokerCard(string customCard, string joker)
    {
        var dictToSend = new Dictionary<string, dynamic>
        {
            ["picked_cards"] = new List<string> {joker, customCard},
        };

        webSocket.Send(JsonConvert.SerializeObject(dictToSend));
    }
}