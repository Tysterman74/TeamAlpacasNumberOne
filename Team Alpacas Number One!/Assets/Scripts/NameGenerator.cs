using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour {

    public string[] names;

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
            name = names[Random.Range(0, names.Length)];
        }
        return name;
        //string
        //while (chosenNames.Contains(
        //return names[Random.Range(0, names.Length)];
    }
}
