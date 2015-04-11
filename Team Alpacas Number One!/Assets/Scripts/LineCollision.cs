using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LineCollision : MonoBehaviour {
    private List<lineObject> trail;
    private Vector2 lastPosition;
    public float maxLength;
    public GameObject trailGraphic;
    private float currentLength = 0;
	// Use this for initialization
	void Start () {
        trail = new List<lineObject>();

        lineObject line = new lineObject(lastPosition, transform.position, trailGraphic);
        trail.Add(line); //add it to the end of our trail list (beginning of in-game trail)
        lastPosition = transform.position;

        currentLength += line.getTranslation().magnitude;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    
	}
}

public class lineObject
{
    public Vector2 start;
    public Vector2 end;
    private GameObject graphic;
    public lineObject(Vector2 start, Vector2 end, GameObject trailGraphicPrefab)
    {
        this.start = start;
        this.end = end;
        Vector2 midpoint = ( start + end )/2;
        this.graphic = trailGraphicPrefab;

    }

    public bool intersects(lineObject other)
    {
        return (checkDir(this.start,this.end,other.start) != checkDir(this.start,this.end,other.end)) 
	&& (checkDir(other.start,other.end,this.start) != checkDir(other.start,other.end,this.end));
    }

    public Vector2 getTranslation()
    {
        return end - start;
    }

    private bool checkDir(Vector2 pt1, Vector2 pt2, Vector2 pt3)
    {
        return ((pt2.x-pt1.x)*(pt3.y-pt1.y)) >= ((pt3.x-pt1.x)*(pt2.y-pt1.y));
    }
}
