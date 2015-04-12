using UnityEngine;
using System.Collections.Generic;

public class ShellBullet : MonoBehaviour {
    private List<LineCollision> enemyTrails;
    private Vector2 lastPosition;
    private Rigidbody rigid;
    public AudioClip hitSound;
	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
        enemyTrails = new List<LineCollision>();
        rigid = GetComponent<Rigidbody>();

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

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            other.gameObject.GetComponent<PlayerState>().loseLife();
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        foreach(LineCollision lines in enemyTrails)
        {
            Vector2? newDirection = lines.destroyIntersection(lastPosition, transform.position);
            if (newDirection != null)
            {
                rigid.velocity = (Vector3)newDirection * rigid.velocity.magnitude;
                transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(((Vector2)newDirection).y, ((Vector2)newDirection).x) * Mathf.Rad2Deg) - 90, Vector3.forward);
                GetComponent<AudioSource>().clip = hitSound;
                GetComponent<AudioSource>().Play();
            }
        }
        lastPosition = transform.position;
	}
}
