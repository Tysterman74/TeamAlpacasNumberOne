using UnityEngine;
using System.Collections;


public class SealPowerUp : PowerUp {
    private GameObject shieldEffect;
    public GameObject shieldEffectPrefab;
	public float duration;
    private GameManager gm;

	// Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>(); 
    }
	
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
                for (int i = 0; i < gm.GetPlayerList().Count; i++)
                {
                    GameObject g = gm.GetPlayerList()[i];
                    if (this.gameObject != g)
                    {
                        Physics.IgnoreCollision(playerHolding.GetComponent<Collider>(), g.GetComponent<Collider>(), false);
                    }
                }
                Destroy(this.gameObject);
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
        for (int i = 0; i < gm.GetPlayerList().Count; i++)
        {
            GameObject g = gm.GetPlayerList()[i];
            if (this.gameObject != g)
            {
                Debug.Log(g);
                Physics.IgnoreCollision(playerHolding.GetComponent<Collider>(), g.GetComponent<Collider>());
            }
        }
	}
}
