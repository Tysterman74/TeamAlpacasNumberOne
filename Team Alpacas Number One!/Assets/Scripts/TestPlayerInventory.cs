using UnityEngine;
using System.Collections;

public class TestPlayerInventory : MonoBehaviour {

    private PowerUp item;
    private bool hasItem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetItem(PowerUp powerUp)
    {
        print("Item get!");
        item = powerUp;
        hasItem = true;
		gameObject.GetComponent<PlayerState> ().setUIItem (item);
    }

    public bool getHasItem()
    {
        return hasItem;
    }

    public void activatePower(GameObject player)
    {
        item.Activate(player);
		item = null;
		hasItem = false;
		gameObject.GetComponent<PlayerState> ().removeUIItem();
    }
}
