using UnityEngine;
using System.Collections.Generic;

public class ShellBullet : MonoBehaviour {
    private List<LineCollision> enemyTrails;
    private Vector2 lastPosition;
    private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
        enemyTrails = new List<LineCollision>();
        rigidbody = GetComponent<Rigidbody>();

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
            Vector2? newDirection = lines.destroyIntersection(lastPosition, transform.position);
            if (newDirection != null)
            {
                rigidbody.velocity = (Vector3)newDirection * rigidbody.velocity.magnitude;
                transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(((Vector2)newDirection).y, ((Vector2)newDirection).x) * Mathf.Rad2Deg) - 90, Vector3.forward);
            }
        }
        lastPosition = transform.position;
	}
}
