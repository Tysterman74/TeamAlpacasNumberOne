using UnityEngine;
using System.Collections;

public class WhalePowerup : PowerUp
{
	public GameObject whaleShockwavePrefab;
	
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{

	}
	
	public override void Activate(GameObject player)
	{
		GameObject whale;
		whale = Instantiate(whaleShockwavePrefab, player.transform.position, player.transform.rotation) as GameObject;
		whale.GetComponent<WhaleShockwaveScript>().setPlayer (player.tag);
		Destroy(this.gameObject);
	}
}
