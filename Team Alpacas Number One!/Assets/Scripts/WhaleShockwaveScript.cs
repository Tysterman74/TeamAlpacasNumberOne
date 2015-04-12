using UnityEngine;
using System.Collections.Generic;

public class WhaleShockwaveScript : MonoBehaviour {
	private Vector3 position;
	public float expandConstant;
	public float maxSize;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SphereCollider collider = gameObject.GetComponent<SphereCollider>();
		//Light light = gameObject.GetComponent<Light>();
		//collider.radius += expandConstant + Time.deltaTime;
        gameObject.transform.localScale += new Vector3(expandConstant + Time.deltaTime, expandConstant + Time.deltaTime, 0);
		//light.range = collider.radius * 2;
		if (gameObject.transform.localScale.x >= maxSize) {
			Destroy(gameObject);
		}
	}

	public void setPlayer(GameObject player){
		Physics.IgnoreCollision (player.GetComponent<Collider>(), gameObject.GetComponent<SphereCollider>());
        Color newColor = player.GetComponent<LineCollision>().trailColor;
        newColor.a = 1;
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
	}
}
