using UnityEngine;
using System.Collections;

public class PlayerName : MonoBehaviour {

    public string playerName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetName(string pName)
    {
        playerName = pName;
    }

    public string GetName()
    {
        return playerName;
    }
}
