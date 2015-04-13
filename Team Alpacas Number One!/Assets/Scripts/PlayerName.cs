using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        print(pName);
        GetComponent<PlayerState>().playerUI.transform.FindChild("PlayerText").GetComponent<Text>().text = pName;
    }

    public string GetName()
    {
        return playerName;
    }
}
