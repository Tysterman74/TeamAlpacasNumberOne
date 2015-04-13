using UnityEngine;
using System.Collections;

public class RandomEngineSound : MonoBehaviour {


    float freq;
    AudioSource engineSound;

	// Use this for initialization
	void Start () {
        freq = Random.Range(5.0f, 20.0f);
        engineSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (freq <= 0)
        {
            engineSound.Play();
            freq = Random.Range(40.0f, 60.0f);
        }
        freq -= Time.deltaTime;
	}
}
