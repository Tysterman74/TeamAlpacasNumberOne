using UnityEngine;
using System.Collections;

public class PufferfishPowerUp : PowerUp {
	PlayerState playerstate;

	// Use this for initialization
	public override void Start () {
		playerstate = GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	public override void Update () {

	}
	
	public override void Activate(GameObject player) {

	}

	void OnCollisionEnter(Collision col)
	{
		playerstate.loseLife();
		Debug.Log ("Enter");
	}
}
