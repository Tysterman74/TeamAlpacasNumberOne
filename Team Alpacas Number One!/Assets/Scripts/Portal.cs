﻿using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public GameObject receivingPortal;
    public Vector2 size;

    public bool leftRight;

    float height;
    float width;

    private AudioSource noise;

	// Use this for initialization
	void Start () {
        noise = GetComponent<AudioSource>();
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
            float distance = (transform.position - receivingPortal.transform.position).magnitude;
            /*Vector3 direction = transform.position - playerPos;
            print("Direction: " + direction + " to " + name);
            float distance = 0;

            if (leftRight)
            {

                if (col.GetComponent<Rigidbody>().velocity.x > 0)
                {
                    playerPos.x -= (width - 0.25f);
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
            }*/
            if (leftRight)
            {
                //playerPos.x = receivingPortal.transform.position.x;
                if (name == "RightPortal")
                {
                    playerPos.x = receivingPortal.transform.position.x + 1.5f;
                }
                else
                {
                    playerPos.x = receivingPortal.transform.position.x - 1.5f;
                }
            }
            else
            {
                //playerPos.y = receivingPortal.transform.position.y;
                if (name == "TopPortal")
                {
                    playerPos.y = receivingPortal.transform.position.y + 1.5f;
                }
                else
                {
                    playerPos.y = receivingPortal.transform.position.y - 1.5f;
                }
            }
            //if (leftRight)
            //{
            //
            //    playerPos.x -= distance;
            //}
            //else
            //{
            //
            //}
            col.transform.position = playerPos;
        }
        col.gameObject.GetComponent<LineCollision>().portalJump();
        noise.Play();
    }
}
