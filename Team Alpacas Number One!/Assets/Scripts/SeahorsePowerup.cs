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
        seahorse = Instantiate(seahorseBulletPrefab, player.transform.position + (speedMultiplier * player.GetComponent<Rigidbody>().velocity), player.transform.rotation) as GameObject;
        Destroy(seahorse, duration);
        seahorse.GetComponent<Rigidbody>().velocity = speedMultiplier * player.GetComponent<Rigidbody>().velocity;
        Physics.IgnoreCollision(seahorse.GetComponent<Collider>(), player.GetComponent<Collider>()); //don't kill yourself when you shoot
        Destroy(this.gameObject);
    }
}
