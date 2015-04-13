using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour {
    private static string[] names = 
    {"Jedd","Ashley","Jebidiah","BaeMax",
    "Arkii",
    "Reddeyfish",
    "Ishni",
    "Pikachu",
    "RNGeezus",
    "Rese",
    "Cyrus",
    "Falco",
    "Chell",
    "Gabe Newell",
    "Coffee Chiu",
    "Ahab",
    "Wily Loop",
    "Ford Loop",
    "Doop Loop"};

    private List<string> chosenNames;
	// Use this for initialization
	void Start () {
        chosenNames = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string GetRandomName()
    {
        string name = names[Random.Range(0, names.Length)];
        while (!chosenNames.Contains(name))
        {
            if (names.Length == 0)
                break;
            name = names[Random.Range(0, names.Length)];
        }
        return name;
        //string
        //while (chosenNames.Contains(
        //return names[Random.Range(0, names.Length)];
    }
}
