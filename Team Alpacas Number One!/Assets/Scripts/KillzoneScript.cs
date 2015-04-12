using UnityEngine;
using System.Collections;

public class KillzoneScript : MonoBehaviour {

	public float timeActivation;
	private bool isActive = false;
	private float timer = 0f;
	private string activePlayer;
	private GameManager gm;

	// Use this for initialization
	void Start () {
		timer += Time.deltaTime;
		
		if (timer > timeActivation) {
			isActive = true;
		}

		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeCollision(){
		foreach(GameObject g in gm.GetPlayerList()){
			Debug.Log (g.name);
			if (activePlayer == g.name)
			{
				Physics.IgnoreCollision(GetComponent<Collider>(),g.GetComponent<Collider>(), true);
			}
			else{
				Physics.IgnoreCollision(GetComponent<Collider>(),g.GetComponent<Collider>(), false);
			}
		}
	}
	public void ChangeActivePlayer(string player){
		activePlayer = player;
	}

	public void OnCollisionEnter(Collision col)
	{
		if (col.rigidbody.tag == "Player" && isActive && activePlayer != col.gameObject.name) {
			Debug.Log(col.gameObject.GetComponent<PlayerState> ().getActivePuffer());
			col.gameObject.GetComponent<PlayerState> ().loseLife ();
		}
	}
}
