using UnityEngine;
using System.Collections;

public class PufferfishPowerUp : PowerUp {
	private string activePlayer;

	public override void Start () {
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override void Activate(GameObject player)
	{
        Color newColor = player.GetComponent<LineCollision>().trailColor;
        newColor.a = 1;
        GetComponent<SpriteRenderer>().material.color = newColor;
		activePlayer = player.name;
		KillzoneScript script = transform.FindChild ("Killzone").GetComponent<KillzoneScript> ();
		script.ChangeActivePlayer (activePlayer);
		script.ChangeCollision ();

	}

}
