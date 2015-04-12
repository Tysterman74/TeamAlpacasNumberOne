using UnityEngine;
using System.Collections.Generic;

public class WhaleShockwaveScript : MonoBehaviour {
	private GameObject player;
	private Vector2 position;
	public float expandConstant;
	public float maxSize;

	// Use this for initialization
	void Start () {
		position = transform.position;
		player = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SphereCollider collider = gameObject.GetComponent<SphereCollider>();
		Light light = gameObject.GetComponent<Light>();
		collider.radius += expandConstant + Time.deltaTime;
		light.range = collider.radius * 2;
		if (collider.radius >= maxSize) {
			Destroy(gameObject);
		}
	}

	public void setPlayer(string playerTag){
		player=GameObject.FindGameObjectWithTag(playerTag);
		Physics.IgnoreCollision (player.GetComponent<BoxCollider>(), gameObject.GetComponent<SphereCollider>());
	}
}
