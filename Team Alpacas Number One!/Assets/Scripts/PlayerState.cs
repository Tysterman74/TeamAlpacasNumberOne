using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
    public int numLives;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {

    }

    public void loseLife()
    {
        numLives -= 1;
        if (numLives < 0)
        {
            Debug.Log("Dead");
        }
    }
}
