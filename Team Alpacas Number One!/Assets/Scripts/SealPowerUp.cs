using UnityEngine;
using System.Collections;


public class SealPowerUp : PowerUp {
    private GameObject shieldEffect;
    public GameObject shieldEffectPrefab;
	public float duration;

	// Use this for initialization

	
	// Update is called once per frame
	public override void Update () {
		if (activated)
		{
			duration -= Time.deltaTime;
			if (duration < 0)
			{
				//Reset
				activated = false;
                Destroy(shieldEffect);
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
        shieldEffect = Instantiate(shieldEffectPrefab, player.transform.position, player.transform.rotation) as GameObject;
        shieldEffect.transform.parent = player.transform;
		activated = true;
		print("Override");
	}
}
