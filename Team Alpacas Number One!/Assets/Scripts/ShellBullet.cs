using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShellBullet : MonoBehaviour {
    private List<LineCollision> enemyTrails;
    private Vector2 lastPosition;
    private Rigidbody rigid;
    public AudioClip hitSound;
    private PlayerState singlePlayer;

    public float homing = 0.02f;
	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
        enemyTrails = new List<LineCollision>();
        rigid = GetComponent<Rigidbody>();

        List<GameObject> enemyObjects = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerList();
        foreach (GameObject other in enemyObjects)
        {
            enemyTrails.Add(other.GetComponent<LineCollision>());
        }
        if (enemyObjects.Count == 1) //single-player mode
        {
            singlePlayer = enemyObjects[0].GetComponent<PlayerState>();
            StartCoroutine("Homing");
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

    public void portalJump()
    {
        lastPosition = transform.position;
    }
    IEnumerator Homing()
    {
        while (true)
        {
            if(!singlePlayer.isDead)
                rigid.velocity = rigid.velocity.magnitude * ((homing * (singlePlayer.transform.position - this.transform.position).normalized) + rigid.velocity).normalized;
            yield return null;
        }
    }

}
