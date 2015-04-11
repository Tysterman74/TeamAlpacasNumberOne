﻿using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
    public int numLives;
    private CameraShakeScript shake;
    // Use this for initialization
	void Start () {
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>();
	}
	
	// Update is called once per frame
    void Update()
    {

    }

    public void loseLife()
    {
        numLives -= 1;
        shake.screenSlam(0.2f,0.5f);
        if (numLives < 0)
        {
            Debug.Log("Dead");
        }
    }
}