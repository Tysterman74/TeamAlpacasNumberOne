using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float turnIncrement;
    public float speedIncrement;
    public float trailIncrement;
    public float incrementTimer = 3.0f;

    private GameObject winText;
    private List<GameObject> itemsOnField;
    private List<GameObject> playerList;

    private GameObject player1;
    private GameObject player2;
    private bool gameFinished;
    private float currentTimer = 0.0f;

    //reminder to self: ask ben about the UI part for this!
    int numPlayers = 2;
    string nthPlayer;

    void Awake()
    {
        //playerList = new List<GameObject>();
        //itemsOnField = new List<GameObject>();
        //for (int i = 1; i <= numPlayers; i++)
        //{ 
        //    
        //}

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        playerList = new List<GameObject>();
        itemsOnField = new List<GameObject>();

        playerList.Add(player1);
        playerList.Add(player2);
    }

	// Use this for initialization
	void Start () {
        winText = GameObject.Find("WinnerObj");
        winText.SetActive(false);

        gameFinished = false;
	}
	
	// Update is called once per frame
	void Update () {
        currentTimer += Time.deltaTime;
        
        if (currentTimer >= incrementTimer && !gameFinished)
        {
            currentTimer = 0.0f;
            increasePlayerSpeed();
        }

	}

    void FixedUpdate()
    {
        if (playerList.Count == 1)
        {
            gameFinished = true;
            //game over
            winText.SetActive(true);
            GameObject g = playerList[0];
            playerList.Clear();

            Text winnerPlayer = winText.transform.FindChild("WinnerPlayer").GetComponent<Text>();
            winnerPlayer.text = g.name;

            Destroy(g);
        }
    }

    void increasePlayerSpeed()
    {
        print("INCREASE MOFO");
        player1.GetComponent<PlayerController>().AddSpeed(speedIncrement);
        player2.GetComponent<PlayerController>().AddSpeed(speedIncrement);
        player1.GetComponent<PlayerController>().AddTurn(turnIncrement);
        player2.GetComponent<PlayerController>().AddTurn(turnIncrement);
        player1.GetComponent<LineCollision>().addTrailLength(trailIncrement);
        player2.GetComponent<LineCollision>().addTrailLength(trailIncrement);
    }

    public void RemovePlayer(GameObject g)
    {
        playerList.Remove(g);
    }

    public List<GameObject> GetPlayerList()
    {
        return playerList;
    }

    public void clearAllItemUI()
    {
        foreach (GameObject g in itemsOnField)
        {
            g.GetComponent<PickupBehaviour>().EraseAllUI(player1, player2);
        }
    }

    public void RemoveItem(GameObject g)
    {
        itemsOnField.Remove(g);
    }

    public void AddItem(GameObject g)
    {
        itemsOnField.Add(g);
    }
}
