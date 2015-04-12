using UnityEngine;
using System.Collections;

public class SealPowerUp : PowerUp {

	public float duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		print("active");
		if (activated)
		{
			print("active");
			duration -= Time.deltaTime;
			if (duration < 0)
			{
				//Reset
				activated = false;

				// change to no longer invincible by enabling player's sphere collider
				playerHolding.GetComponent<PlayerState>().setInvulnerability(false);
			}
		}
	}
	
	public override void Activate(GameObject player)
	{
		playerHolding = player;

		//make player invincible
		playerHolding.GetComponent<PlayerState>().setInvulnerability(true);

		activated = true;
		print("Override");
	}
}
