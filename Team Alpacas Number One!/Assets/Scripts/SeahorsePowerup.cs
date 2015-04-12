using UnityEngine;
using System.Collections;

public class SeahorsePowerup : PowerUp
{
    public float speedMultiplier;
    public float duration;
    public GameObject seahorseBulletPrefab;

    public override void Activate(GameObject player)
    {
        GameObject seahorse;
        seahorse = Instantiate(seahorseBulletPrefab, player.transform.position, player.transform.rotation) as GameObject;
        Destroy(seahorse, duration);
        seahorse.GetComponent<Rigidbody>().velocity = speedMultiplier * player.GetComponent<Rigidbody>().velocity;
        Destroy(this.gameObject);
    }
}
