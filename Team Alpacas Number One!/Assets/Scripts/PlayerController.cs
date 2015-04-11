using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1;
    public float rotaSpeed = 1;
    Rigidbody rb;
    public KeyCode turnLeft;
    public KeyCode turnRight;
    public KeyCode itemUse;

    // Use this for initialization
    void Start()
    {

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

        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.rigidbody.tag == "Player")
        {
            rb.AddRelativeForce(-transform.up*100, ForceMode.Impulse);
        }
    }
}
