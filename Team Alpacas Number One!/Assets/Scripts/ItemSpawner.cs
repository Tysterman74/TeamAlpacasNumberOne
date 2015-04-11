using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour {
	
	public GameObject[] itemlist;
	public int max;
	public float time;
	private float timer =0f;
	private float width;
	private float height;
	// Use this for initialization
	void Start () {
		height = 2.0f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		Debug.Log(width);
		Debug.Log(height);
		if (timer > time && max > GameObject.FindGameObjectsWithTag("Pickup").Length) {
			Instantiate (itemlist [Random.Range (0, itemlist.Length)], new Vector2 (Random.Range (-width/2, width/2), Random.Range (-height/2, height/2)), itemlist [0].transform.rotation);
			timer = 0f;
		}
	}
}
