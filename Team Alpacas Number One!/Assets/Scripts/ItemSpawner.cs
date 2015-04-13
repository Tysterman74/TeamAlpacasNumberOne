using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour 
{
	public List<GameObject> itemlist = new List<GameObject>();
	public int max;
	public float time;
	public float itemDistance;
	public float border;
	public int maxPufferfishCount;
	private float timer = 0f;
	private float width;
	private float height;
    private GameManager gm;
	private GameObject[] currentObjects;
	private int currentPufferfishCount = 0;
	private List<GameObject> newItemList;
    private OffscreenPointer pointer;
	// Use this for initialization
	void Start () 
	{
		height = 2.0f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pointer = GameObject.FindGameObjectWithTag("OffscreenPointer").GetComponent<OffscreenPointer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		currentObjects = GameObject.FindGameObjectsWithTag ("Pickup");
		
		
		if (timer > time && max > currentObjects.Length) {
			int currentPufferfishCount = 0;
			for (int i = 0; i < currentObjects.Length; i++) {
				if(currentObjects[i].name == "Pufferfish(Clone)"){
					currentPufferfishCount +=1;
				}
			}			
			Vector2 itemToPut = new Vector2 (Random.Range ((border - width / 2), (width / 2 - border)), Random.Range ((border - height / 2), (height / 2 - border)));
			if(!(Physics.CheckSphere(itemToPut, itemDistance)))
			{
				if(currentPufferfishCount < maxPufferfishCount){
					GameObject g = (GameObject) Instantiate (itemlist [Random.Range (0, itemlist.Count)], itemToPut, itemlist [0].transform.rotation);
					gm.AddItem(g);
					timer = 0f;
				}
				else{
					newItemList = itemlist;
					foreach(GameObject game in newItemList){
						if (game.name == "Pufferfish"){
							newItemList.Remove(game);
						}
					}
					GameObject g = (GameObject) Instantiate (newItemList [Random.Range (0, itemlist.Count)], itemToPut, itemlist [0].transform.rotation);
					gm.AddItem(g);
					timer = 0f;
				}
			}
			
		}
		if(max <= GameObject.FindGameObjectsWithTag ("Pickup").Length){
			timer = 0f;
		}
	}
}
