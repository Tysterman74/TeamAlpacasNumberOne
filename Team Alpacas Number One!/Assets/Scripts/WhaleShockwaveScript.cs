using UnityEngine;
using System.Collections.Generic;

public class WhaleShockwaveScript : MonoBehaviour {
	private Vector2 position;
	public float expandConstant;
	public float maxSize;

	// Use this for initialization
	void Start () {
		position = transform.position;
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

	public void setPlayer(GameObject player){
		Physics.IgnoreCollision (player.GetComponent<BoxCollider>(), gameObject.GetComponent<SphereCollider>());
	}
}
