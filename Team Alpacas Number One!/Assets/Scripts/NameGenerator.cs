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
        if (chosenNames == null)
        {
            chosenNames = new List<string>();
        }

        string name = names[Random.Range(0, names.Length)];
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
