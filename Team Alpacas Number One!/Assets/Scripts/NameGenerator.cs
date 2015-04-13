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
        if (chosenNames == null)
        {
            chosenNames = new List<string>();
        }

        string name = names[Random.Range(0, names.Length)];
        if (chosenNames.Count != 0)
        {
            while (chosenNames.Contains(name))
            {
                name = names[Random.Range(0, names.Length)];
            }
        }
        //while (!chosenNames.Contains(name))
        //{
        //    name = names[Random.Range(0, names.Length)];
        //}
        chosenNames.Add(name);
        return name;
        //string
        //while (chosenNames.Contains(
        //return names[Random.Range(0, names.Length)];
    }
}
