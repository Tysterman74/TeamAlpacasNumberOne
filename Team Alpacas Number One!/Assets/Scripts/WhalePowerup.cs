using UnityEngine;
using System.Collections;

public class WhalePowerup : PowerUp
{
	public GameObject whaleShockwavePrefab;

	public override void Activate(GameObject player)
	{
		GameObject whale;
		whale = Instantiate(whaleShockwavePrefab, player.transform.position, player.transform.rotation) as GameObject;
		whale.GetComponent<WhaleShockwaveScript>().setPlayer (player);
		Destroy(this.gameObject);
	}
}
