using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;

    private float incrementTimer = 3.0f;
    private float currentTimer = 0.0f;

	// Use this for initialization
	void Start () {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
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
        player1.GetComponent<PlayerController>().AddSpeed(0.75f);
        player2.GetComponent<PlayerController>().AddSpeed(0.75f);
        player1.GetComponent<PlayerController>().AddTurn(0.35f);
        player2.GetComponent<PlayerController>().AddTurn(0.35f);
    }
}
