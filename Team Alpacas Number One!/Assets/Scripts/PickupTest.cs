using UnityEngine;
using System.Collections.Generic;

public class PickupTest : MonoBehaviour {
    Dictionary<int, Vector2> currentPositions; //int is player ID, float is their relative position
    Dictionary<int, float>totalAngles; //int is player ID, float is their angle

    //need to combine these


	// Use this for initialization
	void Start () {
        currentPositions = new Dictionary<int, Vector2>();
        totalAngles = new Dictionary<int, float>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        totalAngles[1] = 0;
        currentPositions[1] = other.transform.position - this.transform.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //don't use Vector2.Angle; it doesn't return negative values
        totalAngles[1] += getAngleBetweenVectors(other.transform.position - this.transform.position, currentPositions[1]);
        Debug.Log(totalAngles[1]);
        currentPositions[1] = other.transform.position - this.transform.position;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        totalAngles.Remove(1);
        currentPositions.Remove(1);
    }

    private float getAngleBetweenVectors(Vector2 vectorNew, Vector2 vectorOld)
    {
        if (Mathf.Abs(Mathf.Atan2(vectorNew.x, vectorNew.y) - Mathf.Atan2(vectorOld.x, vectorOld.y)) > 1)
        {
            //Debug.Log(vectorNew);
            //Debug.Log(vectorOld);
            if (Mathf.Sign(vectorNew.y) < 0 && Mathf.Sign(vectorOld.y) < 0)
            {
                //Debug.Log("ping!");
                //if (Mathf.Sign(vectorNew.x) <= 0 && Mathf.Sign(vectorOld.x) >= 0) //special case; crossing from pi to -pi
                    //Debug.Log(Vector2.Angle(vectorNew, vectorOld));
                //if (Mathf.Sign(vectorNew.x) >= 0 && Mathf.Sign(vectorOld.x) <= 0) //special case; crossing from -pi to pi
                   // Debug.Log(-Vector2.Angle(vectorNew, vectorOld));
            }
        }
        if (Mathf.Sign(vectorNew.y) < 0 && Mathf.Sign(vectorOld.y) < 0)
        {
            if (Mathf.Sign(vectorNew.x) < 0 && Mathf.Sign(vectorOld.x) > 0) //special case; crossing from pi to -pi
                return Vector2.Angle(vectorNew, vectorOld);
            if (Mathf.Sign(vectorNew.x) > 0 && Mathf.Sign(vectorOld.x) < 0) //special case; crossing from -pi to pi
                return -Vector2.Angle(vectorNew, vectorOld);
        }

        return (Mathf.Atan2(vectorNew.x, vectorNew.y) - Mathf.Atan2(vectorOld.x, vectorOld.y)) * Mathf.Rad2Deg;
    }

}
