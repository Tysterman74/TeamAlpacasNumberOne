using UnityEngine;
using System.Collections;

public class PufferfishPowerUp : PowerUp {
	public float timeActivation;
	private float timer = 0f;
	private bool isActive = false;
	private string activePlayer;
	private GameManager gm;
	// Use this for initialization
	public override void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	public override void Update () {
		timer += Time.deltaTime;

		if (timer > timeActivation) {
			isActive = true;
		}
	}
	
	public override void Activate(GameObject player)
	{
	}


	public void OnCollisionEnter(Collision col)
	{
		if (col.rigidbody.tag == "Player" && isActive && !col.gameObject.GetComponent<PlayerState> ().getActivePuffer()) {
			Debug.Log(col.gameObject.GetComponent<PlayerState> ().getActivePuffer());
			col.gameObject.GetComponent<PlayerState> ().loseLife ();
		}
	}
	public void ActivePlayer(GameObject player){
		activePlayer = player.name;
	}
}
