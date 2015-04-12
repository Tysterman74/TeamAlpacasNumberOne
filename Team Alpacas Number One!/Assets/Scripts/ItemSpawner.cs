using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour 
{
	public GameObject[] itemlist;
	public int max;
	public float time;
	public float itemDistance;
	public float border;
	private float timer = 0f;
	private float width;
	private float height;
    private GameManager gm;

	// Use this for initialization
	void Start () 
	{
		height = 2.0f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer > time && max > GameObject.FindGameObjectsWithTag ("Pickup").Length) {
			Vector2 itemToPut = new Vector2 (Random.Range ((border - width / 2), (width / 2 - border)), Random.Range ((border - height / 2), (height / 2 - border)));
			if(!(Physics.CheckSphere(itemToPut, itemDistance)))
			{
				GameObject g = (GameObject) Instantiate (itemlist [Random.Range (0, itemlist.Length)], itemToPut, itemlist [0].transform.rotation);
                gm.AddItem(g);
                timer = 0f;
			}
		}
		if(max <= GameObject.FindGameObjectsWithTag ("Pickup").Length){
			timer = 0f;
		}
	}
}
