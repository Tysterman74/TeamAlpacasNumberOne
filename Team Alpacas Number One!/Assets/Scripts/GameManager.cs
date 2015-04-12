using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public float turnIncrement;
    public float speedIncrement;
    public float trailIncrement;
    public float incrementTimer = 3.0f;

    private List<GameObject> itemsOnField;
    private List<GameObject> playerList;
    
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    private float currentTimer = 0.0f;

    //reminder to self: ask ben about the UI part for this!
    public int numPlayers = 2;
    string nthPlayer;

    void Awake()
    {
        Debug.Log("awaken!");
        playerList = new List<GameObject>();
        itemsOnField = new List<GameObject>();
        player1 = Instantiate(Resources.Load("player1", typeof(GameObject))) as GameObject;
        player2 = Instantiate(Resources.Load("player2", typeof(GameObject))) as GameObject;
        /*Instantiate(player1, new Vector3(-5.0f, 4.0f, 0.0f), Quaternion.identity);
        Instantiate(player2, new Vector3(5.0f, 4.0f, 0.0f), Quaternion.identity);
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");*/
        playerList.Add(player1);
        playerList.Add(player2);
        if(numPlayers == 3)
        {
            player3 = Instantiate(Resources.Load("player3", typeof(GameObject))) as GameObject;
            player3 = GameObject.Find("Player3");
            playerList.Add(player3);
        }
            
        if(numPlayers == 4)
        {
            player4 = Instantiate(Resources.Load("player4", typeof(GameObject))) as GameObject;
            player4 = GameObject.Find("Player4");
            playerList.Add(player4);
        }
            
    }

	// Use this for initialization
	void Start () {
        /*Instantiate(player1, new Vector3(-5.0f, 4.0f, 0.0f), Quaternion.identity);
        Instantiate(player2, new Vector3(5.0f, 4.0f, 0.0f), Quaternion.identity);
        Instantiate(player3, new Vector3(-5.0f, -4.0f, 0.0f), Quaternion.identity);
        Instantiate(player4, new Vector3(5.0f, -4.0f, 0.0f), Quaternion.identity);
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player3 = GameObject.Find("Player3");
        player4 = GameObject.Find("Player4");

        playerList = new List<GameObject>();
        itemsOnField = new List<GameObject>();

        playerList.Add(player1);
        playerList.Add(player2);
        playerList.Add(player3);
        playerList.Add(player4);*/
	}
	
	// Update is called once per frame
	void Update () {
        currentTimer += Time.deltaTime;
        if (currentTimer >= incrementTimer)
        {
            currentTimer = 0.0f;
            increasePlayerSpeed();
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

        if (numPlayers == 3)
        { 
            player3.GetComponent<PlayerController>().AddSpeed(speedIncrement);
            player3.GetComponent<PlayerController>().AddTurn(turnIncrement);
            player3.GetComponent<LineCollision>().addTrailLength(trailIncrement);
        }

        if(numPlayers == 4)
        {
            player4.GetComponent<PlayerController>().AddSpeed(speedIncrement);
            player4.GetComponent<PlayerController>().AddTurn(turnIncrement);
            player4.GetComponent<LineCollision>().addTrailLength(trailIncrement);
        }
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
