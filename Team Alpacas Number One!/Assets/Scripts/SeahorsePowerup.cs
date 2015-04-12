using UnityEngine;
using System.Collections;

public class SeahorsePowerup : PowerUp
{
    public float speedMultiplier;
    public float duration;
    public GameObject seahorseBulletPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            duration -= Time.deltaTime;
            if (duration < 0)
            {
                //Reset
                activated = false;
                playerHolding.GetComponent<PlayerController>().SetBonusSpeed(0);

            }
        }
    }

    public override void Activate(GameObject player)
    {
        GameObject seahorse;
        seahorse = Instantiate(seahorseBulletPrefab, player.transform.position, player.transform.rotation) as GameObject;
        Destroy(seahorse, 5);
        seahorse.GetComponent<Rigidbody>().velocity = speedMultiplier * player.GetComponent<Rigidbody>().velocity;
        Destroy(this.gameObject);
    }
}
