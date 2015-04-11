using UnityEngine;
using System.Collections.Generic;

public class PickupBehaviour : MonoBehaviour {

    public GameObject loopUIElement;

    Dictionary<int, loopObject> currentPositions; //int is player ID, float is their relative position

    // Use this for initialization
    void Start()
    {
        currentPositions = new Dictionary<int, loopObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != Tags.player)
            return;
        GameObject UI;
        UI = Instantiate(loopUIElement, this.transform.position, Quaternion.LookRotation(other.transform.position - this.transform.position, Vector3.forward)) as GameObject; //forward is (0,0,1)
        currentPositions[1] = new loopObject(other.transform.position - this.transform.position, UI);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != Tags.player)
            return;
        currentPositions[1].updatePosition(other.transform.position - this.transform.position);

        if (currentPositions[1].loopComplete())
        {
            Debug.Log("LOOP COMPLETE!");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != Tags.player)
            return;
        currentPositions.Remove(1);
    }

    public class loopObject
    {
        public Vector2 currentPosition;
        public float totalAngleCovered;
        public Vector2 startPosition;
        private GameObject UIArc;
        public loopObject(Vector2 position, GameObject UIArc)
        {
            startPosition = position;
            currentPosition = position;
            totalAngleCovered = 0;
            this.UIArc = UIArc;
        }

        public void updatePosition(Vector2 position)
        {
            totalAngleCovered += getAngleBetweenVectors(position, currentPosition);
            currentPosition = position;
        }

        public bool loopComplete()
        {
            return Mathf.Abs(totalAngleCovered) >= 360;
        }

        private float getAngleBetweenVectors(Vector2 vectorNew, Vector2 vectorOld)
        {
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
}

