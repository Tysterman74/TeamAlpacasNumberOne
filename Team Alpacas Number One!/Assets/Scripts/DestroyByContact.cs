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

        if ((col.gameObject.tag.Contains("Player") || col.gameObject.tag.Contains("TrailMaker")) && !playerstate.isDead)
        {
            playerstate.loseLife();
        }
    }
}
