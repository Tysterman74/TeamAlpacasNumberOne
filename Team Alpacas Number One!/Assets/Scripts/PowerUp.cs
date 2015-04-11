using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public string name;

    private GameObject player;

	// Use this for initialization
    public PowerUp()
    {

    }

    public virtual void Activate()
    {

    }

    public virtual void PrintPowerUp()
    {
        print(name);
    }


}
