using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1;
    public float rotaSpeed = 1;
    Rigidbody rb;
    public KeyCode turnLeft;
    public KeyCode turnRight;
    public KeyCode itemUse;

    private TestPlayerInventory inventory;
    private float bonusSpeed;

    // Use this for initialization
    void Start()
    {
        //TEMPORARY
        inventory = GetComponent<TestPlayerInventory>();
        bonusSpeed = 0;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //DirResult = new Vector3(0.0f, 1.0f, 0.0f);
        if (Input.GetKey(turnLeft))
        {
            rb.rotation = 
                Quaternion.Euler(GetComponent<Rigidbody>().rotation.eulerAngles + new Vector3(0.0f, 0.0f, rotaSpeed));
        }
        else if (Input.GetKey(turnRight))
        {
            rb.rotation =
                Quaternion.Euler(GetComponent<Rigidbody>().rotation.eulerAngles + new Vector3(0.0f, 0.0f, -rotaSpeed));
        }

        if (Input.GetKeyUp(itemUse))
        {
            if (inventory.getHasItem())
            {
                inventory.activatePower(this.gameObject);
            }
            else
            {
                print("No item fool!");
            }
        }
        rb.velocity = transform.up * (speed + bonusSpeed);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.rigidbody.tag == "Player")
        {
            rb.AddRelativeForce(-transform.up*100, ForceMode.Impulse);
        }
    }

    public void AddSpeed(float increment)
    {
        speed += increment;
    }

    public void SetBonusSpeed(float bonus) 
    {
        bonusSpeed = bonus;
    }
}
