using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    PlayerState playerstate;

    void Start()
    {
        playerstate = GetComponent<PlayerState>();
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.rigidbody.tag.Contains("Player") /*&& !playerstate.isDead*/)
        {
            playerstate.loseLife();
        }
    }
}
