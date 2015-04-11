using UnityEngine;
using System.Collections;

public class HedgehogPowerUp : PowerUp {

    public float bonusSpeed;
    public float duration;

    private Vector3 originalVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
        player.GetComponent<PlayerController>().SetBonusSpeed(bonusSpeed);
        activated = true;
        //player.GetComponent<Rigidbody>().velocity += transform.up * bonusSpeed;
        print("Override");
    }
}
