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

    public void destroyIntersection(Vector2 start, Vector2 end)
    {
        line otherLine = new line(start, end);
        bool hit = true;
        while (hit)
        {
            hit = false;
            for (int i = 0; i < trail.Count; i++)
            {
                if (trail[i].intersects(otherLine))
                {
                    Debug.Log("hi");
                    hit = true;
                    int firstIndex = i - 10;
                    if (firstIndex < 0)
                        firstIndex = 0;
                    int count = 80;
                    if (count + firstIndex >= trail.Count)
                        count = (trail.Count - firstIndex) - 1;
                    for (int j = firstIndex; j < firstIndex + count; j++)
                        trail[j].destroy();
                    trail.RemoveRange(firstIndex, count);
                    break;
                }
                
            }
        }
    }

    public void portalJump()
    {
        lastPosition = transform.position;
    }

    public void clearTrail()
    {
        foreach (lineObject line in trail)
        {
            line.destroy();
        }
        trail = new List<lineObject>();
        currentLength = 0;
    }
    public class line
    {
        public Vector2 start;
        public Vector2 end;
        public line(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public bool intersects(line other)
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
    }
    public class lineObject : line
    {
        private GameObject graphic;
        public lineObject(Vector2 start, Vector2 end, GameObject trailGraphicPrefab) : base(start, end)
        {
            Vector2 midpoint = (start + end) / 2;
            Vector2 translation = getTranslation();
            this.graphic = trailGraphicPrefab;
            graphic.transform.Translate(midpoint, Space.World);
            graphic.transform.Rotate(0, 0, 90 - (180 * Mathf.Atan2(translation.x, translation.y) / Mathf.PI), Space.World);
            graphic.transform.localScale = new Vector3(translation.magnitude * 2, 1, 1);

        }

        public void destroy()
        {
            GameObject.Destroy(this.graphic);
        }
    }

}

