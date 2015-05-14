using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AITrail : MonoBehaviour {
    private Rigidbody rigid;
    public float rotaSpeed = 1.0f;
    public float rotationPeriod = 5.0f;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        List<GameObject> playerList = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerList();
        foreach (GameObject player in playerList)
        {
                player.GetComponent<LineCollision>().addEnemy(GetComponent<LineCollision>());
        }
	}



    IEnumerator Turn()
    {
        while (true)
        {
            int Duration = (int)(300 * Random.value);
            Debug.Log("turning");
            if (Random.value > 0.5) //50% chance
            {
                //turn right
                for (int i = 0; i < Duration; i++)
                {
                    Quaternion q = Quaternion.AngleAxis(rotaSpeed * Time.deltaTime, transform.forward) * transform.rotation;
                    rigid.MoveRotation(q);
                    float mag = rigid.velocity.magnitude;
                    yield return null;
                }
                    
            }
            else
            {
                //turn left
                for (int i = 0; i < Duration; i++)
                {
                    rigid.rotation =
                        Quaternion.Euler(rigid.rotation.eulerAngles + new Vector3(0.0f, 0.0f, rotaSpeed * Time.deltaTime));
                    yield return null;
                }
            }
            yield return new WaitForSeconds((2 * rotationPeriod * Random.value));
            
        }
    }
}
