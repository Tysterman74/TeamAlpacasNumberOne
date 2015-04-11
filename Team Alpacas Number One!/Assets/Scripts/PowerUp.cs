using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public string name;

    //Variable to show that it has been activated.
    protected bool activated;

    //Variable to hold the player that has the power up.
    //USE THIS WHEN IMPLEMENTING THE FUNCTIONALITY OF THE PLAYER
    protected GameObject playerHolding;

	// Use this for initialization
    public PowerUp()
    {

    }

    //Function to be overriden
    public virtual void Start()
    {

    }

    //Function to be overriden when creating own script
    public virtual void Update()
    {
        print("hi");
    }

    //Function to be overriden when creating your own script
    public virtual void Activate(GameObject player)
    {

    }

    public virtual void PrintPowerUp()
    {
        print(name);
    }


}
