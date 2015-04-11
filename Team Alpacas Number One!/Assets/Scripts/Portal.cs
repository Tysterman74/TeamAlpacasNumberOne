using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public GameObject receivingPortal;
    public Vector2 size;

    float height;
    float width;

	// Use this for initialization
	void Start () {
        GetComponent<BoxCollider2D>().size = size;
        Camera cam = Camera.main;
        height = 2.0f * cam.orthographicSize;
        width = height * cam.aspect;
        print("Width: " + width);
        print("Height: " + height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Teleport player to other side.
            //Assume width for now,
            Vector3 playerPos = col.transform.position;
            if (col.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                playerPos.x -= (width - 1);
            }
            else
            {
                playerPos.x += (width - 1);
            }
            col.transform.position = playerPos;
        }
    }
}
