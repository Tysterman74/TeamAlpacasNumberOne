using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public GameObject receivingPortal;
    public Vector2 size;

    public bool leftRight;

    float height;
    float width;

	// Use this for initialization
	void Start () {
        GetComponent<BoxCollider>().size = size;
        Camera cam = Camera.main;
        height = 2.0f * cam.orthographicSize;
        width = height * cam.aspect;
        print("Width: " + width);
        print("Height: " + height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Player"))
        {
            //Teleport player to other side.
            //Assume width for now,
            Vector3 playerPos = col.transform.position;
            if (leftRight)
            {
                if (col.GetComponent<Rigidbody>().velocity.x > 0)
                {
                    playerPos.x -= (width - 1);
                }
                else
                {
                    playerPos.x += (width);
                }
            }
            else
            {
                if (col.GetComponent<Rigidbody>().velocity.y > 0)
                {
                    playerPos.y -= (height);
                }
                else
                {
                    playerPos.y += (height);
                }
            }
            col.transform.position = playerPos;
        }
        col.gameObject.GetComponent<LineCollision>().portalJump();
    }
}
