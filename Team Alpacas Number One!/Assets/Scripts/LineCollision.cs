using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LineCollision : MonoBehaviour {
    private List<lineObject> trail;
    private Vector2 lastPosition;
    public float maxLength;
    public GameObject trailGraphic;
    private float currentLength = 0;
    private List<LineCollision> enemyTrails;

    private bool flagIgnoreNext = false;
    private PlayerState state; //reference to our playerstatescript
	// Use this for initialization
	void Start () {
        trail = new List<lineObject>();
        lastPosition = transform.position;
        enemyTrails = new List<LineCollision>();
        state = GetComponent<PlayerState>();

        //get list of enemy players
        int playerNumber = 1;
        GameObject other = GameObject.FindGameObjectWithTag("Player" + playerNumber);
        while(other)
        {
            if (other != this.gameObject)
            {
                enemyTrails.Add(other.GetComponent<LineCollision>());
            }
            playerNumber++;
            try
            {
                other = GameObject.FindGameObjectWithTag("Player" + playerNumber);
            }
            catch
            {
                break;
            }
        }
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        lineObject line = new lineObject(lastPosition, transform.position, Instantiate(trailGraphic) as GameObject);
        trail.Add(line); //add it to the end of our trail list (beginning of in-game trail)

        foreach (LineCollision other in enemyTrails)
        {
            if (other.doesSegmentIntersect(line))
            {
                Debug.Log("HIT!");
                state.loseLife();
            }
        }
        currentLength += line.getTranslation().magnitude;
        lastPosition = transform.position;
        while (currentLength > maxLength) //trail length limitation
        {
            currentLength = currentLength - trail[0].getTranslation().magnitude;
            trail[0].destroy();
            trail.RemoveAt(0);
        }
    }

    public bool doesSegmentIntersect(lineObject other)
    {
        foreach (lineObject trailLine in trail)
        {
            if (trailLine.intersects(other))
                return true;
        }

        return false;
    }

    public void portalJump()
    {
        lastPosition = transform.position;
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
            Vector2 midpoint = (start + end) / 2;
            Vector2 translation = getTranslation();
            this.graphic = trailGraphicPrefab;
            graphic.transform.Translate(midpoint, Space.World);
            graphic.transform.Rotate(0, 0, 90 - (180 * Mathf.Atan2(translation.x, translation.y) / Mathf.PI), Space.World);
            graphic.transform.localScale = new Vector3(translation.magnitude * 2, 1, 1);

        }

        public bool intersects(lineObject other)
        {
            return (checkDir(this.start, this.end, other.start) != checkDir(this.start, this.end, other.end))
        && (checkDir(other.start, other.end, this.start) != checkDir(other.start, other.end, this.end));
        }

        public Vector2 getTranslation()
        {
            return end - start;
        }

        private bool checkDir(Vector2 pt1, Vector2 pt2, Vector2 pt3)
        {
            return ((pt2.x - pt1.x) * (pt3.y - pt1.y)) > ((pt3.x - pt1.x) * (pt2.y - pt1.y));
        }

        public void destroy()
        {
            GameObject.Destroy(this.graphic);
        }
    }

}

