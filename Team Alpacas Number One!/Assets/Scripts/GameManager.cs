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
    private GameObject player3;
    private GameObject player4;
    private NameGenerator names;
    private bool gameFinished;
    private float currentTimer = 0.0f;

    //reminder to self: ask ben about the UI part for this!
    public int numPlayers = 2;
    string nthPlayer;

    void Awake()
    {
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
        if(numPlayers >= 3)
        {
            player3 = Instantiate(Resources.Load("player3", typeof(GameObject))) as GameObject;
            //player3 = GameObject.Find("Player3");
            playerList.Add(player3);
        }
            
        if(numPlayers == 4)
        {
            player4 = Instantiate(Resources.Load("player4", typeof(GameObject))) as GameObject;
            //player4 = GameObject.Find("Player4");
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
        names = GetComponent<NameGenerator>();

		GameObject playerFrame = GameObject.Find ("CanvasPrefab/PlayerFrame");
		playerFrame.GetComponent<UIScaleScript> ().setUI (numPlayers);
        winText = GameObject.Find("WinnerObj");
        winText.SetActive(false);

        gameFinished = false;

        player1.GetComponent<PlayerName>().SetName(names.GetRandomName());
        player2.GetComponent<PlayerName>().SetName(names.GetRandomName());

        if (numPlayers >= 3)
            player3.GetComponent<PlayerName>().SetName(names.GetRandomName());
        if (numPlayers == 4)
            player4.GetComponent<PlayerName>().SetName(names.GetRandomName());
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

            StartCoroutine(exitToMenu());
            Text winnerPlayer = winText.transform.FindChild("WinnerPlayer").GetComponent<Text>();
            winnerPlayer.text = g.GetComponent<PlayerName>().GetName();
            GameObject.FindGameObjectWithTag("Fireworks").GetComponent<ParticleSystem>().Play();

            Destroy(g);
        }

        if (playerList.Count == 0)
        {
            gameFinished = true;
            //game over
            winText.SetActive(true);
            playerList.Clear();

            StartCoroutine(exitToMenu());
            Text winnerPlayer = winText.transform.FindChild("WinnerPlayer").GetComponent<Text>();
            winnerPlayer.text = "NO ONE WINS";
        }
    }

    IEnumerator exitToMenu()
    {
        yield return new WaitForSeconds(5.0f);
        Application.LoadLevel("MainMenu");
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

        if (numPlayers >= 3)
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
