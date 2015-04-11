using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1;
    public float rotaSpeed = 1;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //DirResult = new Vector3(0.0f, 1.0f, 0.0f);
        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation = 
                Quaternion.Euler(GetComponent<Rigidbody>().rotation.eulerAngles + new Vector3(0.0f, 0.0f, rotaSpeed));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.rotation =
                Quaternion.Euler(GetComponent<Rigidbody>().rotation.eulerAngles + new Vector3(0.0f, 0.0f, -rotaSpeed));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

        }

        rb.velocity = transform.up * speed;
    }
}
