using UnityEngine;
using System.Collections;

public class PufferfishPowerUp : PowerUp {
	bool isActive = false;

	// Use this for initialization
	public override void Start () {
	}
	
	// Update is called once per frame
	public override void Update () {

	}
	
	public override void Activate(GameObject player) {

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.rigidbody.tag == "Player") {
			col.gameObject.GetComponent<PlayerState> ().loseLife ();
			isActive = true;
		}
	}
}
