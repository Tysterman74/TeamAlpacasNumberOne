using UnityEngine;
using System.Collections.Generic;

public class seahorseBulletScript : MonoBehaviour {
    private List<LineCollision> enemyTrails;
    private Vector2 lastPosition;
    public AudioClip hitSound;
	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
        enemyTrails = new List<LineCollision>();

        List<GameObject> enemyObjects = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerList();
        foreach (GameObject other in enemyObjects)
        {
            enemyTrails.Add(other.GetComponent<LineCollision>());
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
            if (lines.destroyIntersection(lastPosition, transform.position) != null)

            {
                GetComponent<AudioSource>().clip = hitSound;
                GetComponent<AudioSource>().Play();
            }
        }
        lastPosition = transform.position;
	}
}
