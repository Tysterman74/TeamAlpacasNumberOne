using UnityEngine;
using System.Collections;

public class ShellPowerup : PowerUp
{
    public float speedMultiplier;
    public float duration;
    public GameObject shellBulletPrefab;


    public override void Activate(GameObject player)
    {
        GameObject shell;
        shell = Instantiate(shellBulletPrefab, player.transform.position + (0.25f * player.GetComponent<Rigidbody>().velocity.normalized), player.transform.rotation) as GameObject;
        Destroy(shell, duration);
        shell.GetComponent<Rigidbody>().velocity = speedMultiplier * player.GetComponent<Rigidbody>().velocity;
        Physics.IgnoreCollision(shell.GetComponent<Collider>(), player.GetComponent<Collider>());
        Destroy(this.gameObject);
    }
}
