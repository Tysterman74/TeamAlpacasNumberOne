using UnityEngine;
using System.Collections;

public class BackgroundSwitching : MonoBehaviour {

    private Camera cam;

    public GameObject picture1;
    public GameObject picture2;
    public GameObject picture3;
    public bool moveRight;

    private GameObject resetCheckPoint;

    private Vector3 originalPic1Pos;
    private Vector3 originalPic2Pos;
    
    private float picWidth;
    private float portalWidth;

    private float camWidth;
    private float camHeight;

    private float arenaWidth;


    private SpriteRenderer pic1Rend;
    private SpriteRenderer pic2Rend;

    private GameObject leftPortal;
    private GameObject rightPortal;
	// Use this for initialization
	void Start () {
        leftPortal = GameObject.Find("LeftPortal");
        rightPortal = GameObject.Find("RightPortal");
        resetCheckPoint = transform.FindChild("ResetCheckPoint").gameObject;

        originalPic1Pos = picture1.transform.position;
        //Use this for reseting.
        originalPic2Pos = picture2.transform.position;

        arenaWidth = Vector2.Distance(new Vector2(leftPortal.transform.position.x, leftPortal.transform.position.y),
            new Vector2(rightPortal.transform.position.x, rightPortal.transform.position.y));

        cam = Camera.main;
        camHeight = 2.0f * Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        //Testing the sprite width
        picWidth = picture1.GetComponent<SpriteRenderer>().bounds.size.x;
        portalWidth = rightPortal.GetComponent<BoxCollider>().size.x / 2.0f;

        pic1Rend = picture1.GetComponent<SpriteRenderer>();
        pic2Rend = picture2.GetComponent<SpriteRenderer>();
        //print("Width: " + width);
	}

    void FixedUpdate()
    {

        if (moveRight)
        {

            if (picture1.transform.position.x - picWidth / 2.0f >= resetCheckPoint.transform.position.x)
            {
                picture1.transform.position = originalPic1Pos + new Vector3(0.075f, 0, 0);
            }

            if (picture2.transform.position.x - picWidth / 2.0f >= resetCheckPoint.transform.position.x)
            {
                picture2.transform.position = originalPic1Pos + new Vector3(0.075f, 0, 0);
            }

            if (picture3.transform.position.x - picWidth / 2.0f >= resetCheckPoint.transform.position.x)
            {
                picture3.transform.position = originalPic1Pos + new Vector3(0.075f, 0, 0);
            }
            picture1.transform.position += new Vector3(0.075f, 0, 0);
            picture2.transform.position += new Vector3(0.075f, 0, 0);
            picture3.transform.position += new Vector3(0.075f, 0, 0);
            //if (!pic1Rend.isVisible)
            //{
            //    picture1.transform.position = originalPic2Pos;
            //}
            //
            //if (!pic2Rend.isVisible)
            //{
            //    picture2.transform.position = originalPic2Pos;
            //}
            //if (picture1.transform.position.x - picWidth/2.0f > rightPortal.transform.position.x - portalWidth)
            //{
            //    picture1.transform.position = originalPic2Pos;
            //}
            //
            //if (picture2.transform.position.x - picWidth/2.0f > rightPortal.transform.position.x - portalWidth)
            //{
            //    picture2.transform.position = originalPic2Pos;
            //}
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
