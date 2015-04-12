using UnityEngine;
using System.Collections.Generic;

public class seahorseBulletScript : MonoBehaviour {
    private List<LineCollision> enemyTrails;
    private Vector2 lastPosition;
	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
        enemyTrails = new List<LineCollision>();

        int playerNumber = 1;
        GameObject other = GameObject.FindGameObjectWithTag("Player" + playerNumber);
        while (other)
        {
            enemyTrails.Add(other.GetComponent<LineCollision>());
            playerNumber++;
            try
            {
                other = GameObject.FindGameObjectWithTag("Player" + playerNumber);
            }
            catch
            {
                break;
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach(LineCollision lines in enemyTrails)
        {
            lines.destroyIntersection(lastPosition, transform.position);
        }
        lastPosition = transform.position;
	}
}
