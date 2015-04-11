using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public string name;

    protected bool activated;
    protected GameObject playerHolding;

	// Use this for initialization
    public PowerUp()
    {

    }

    public virtual void Activate(GameObject player)
    {

    }

    public virtual void PrintPowerUp()
    {
        print(name);
    }


}
