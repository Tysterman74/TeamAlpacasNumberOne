using UnityEngine;
using System.Collections;

public class TylersTestMovement : MonoBehaviour {

    private float speed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().velocity = new Vector2(speed, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().velocity = new Vector2(-speed, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().velocity = new Vector2(0, -speed);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
