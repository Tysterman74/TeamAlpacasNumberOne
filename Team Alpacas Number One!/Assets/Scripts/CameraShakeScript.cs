using UnityEngine;
using System.Collections;

public class CameraShakeScript : MonoBehaviour
{
    public float smoothing = 10f;
    public float sizeBuffer = 1f;
    public float portalBuffer = 3f;
    public float zScalar = -1.0f;
    private GameObject[] players;
    private Camera cam;
    private Resolution resolution;
    private Transform holder; //move the holder for normal movement, not the camera
    private Transform leftPortal;
    private Transform rightPortal;
    private Transform topPortal;
    private Transform bottomPortal;
    // How long the object should shake for.
    public float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public Vector3 originalPos;

    private bool shakeStart;
    private int screenShakeFramesLeft = 0;
    private float screenShakeIntensity = 0;
    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        cam = GetComponent<Camera>();
        resolution = Screen.currentResolution; // may need to do checking to check if resolution has been changed
        holder = this.transform.parent;
        holder.position = new Vector3(holder.position.x,holder.position.y,cam.orthographicSize);
        leftPortal = holder.parent.Find("LeftPortal");
        rightPortal = holder.parent.Find("RightPortal");
        topPortal = holder.parent.Find("TopPortal");
        bottomPortal = holder.parent.Find("BottomPortal");
    }

    void FixedUpdate()
    {
        float minx = holder.position.x;
        float maxx = holder.position.x;
        float miny = holder.position.y;
        float maxy = holder.position.y;

        foreach (GameObject player in players)
        {
            Vector3 playerPos = player.transform.position;
            if (minx < leftPortal.position.x || maxx > rightPortal.position.x || miny < bottomPortal.position.y || maxy > topPortal.position.y)
            {
                // do nothing; the plane is outside the portals
            }
            else
            {
                if (minx > playerPos.x)
                    minx = playerPos.x;
                if (maxx < playerPos.x)
                    maxx = playerPos.x;
                if (miny > playerPos.y)
                    miny = playerPos.y;
                if (maxy < playerPos.y)
                    maxy = playerPos.y;
            }
            
            
        }

        minx -= sizeBuffer;
        maxx += sizeBuffer;
        miny -= sizeBuffer;
        maxy += sizeBuffer;

        //now bind to the screen
        if (minx < leftPortal.position.x)
            minx = leftPortal.position.x + portalBuffer;
        if (maxx > rightPortal.position.x)
            maxx = rightPortal.position.x - portalBuffer;
        if (miny < bottomPortal.position.y)
            miny = bottomPortal.position.y + portalBuffer;
        if (maxy > topPortal.position.y)
            maxy = topPortal.position.y - portalBuffer;

        float midx = (minx + maxx) * 0.5f;
        float midy = (miny + maxy) * 0.5f;

        float xrange = Mathf.Abs(minx - maxx);
        float yrange = Mathf.Abs(miny - maxy);
        float z = holder.position.z;
        
        if (xrange / resolution.width > yrange / resolution.height)
        {
            z = xrange;
            if (midy + Mathf.Abs(zScalar) * Mathf.Abs(z) > topPortal.position.y)
            {
                midy = topPortal.position.y - (Mathf.Abs(zScalar) * Mathf.Abs(z));
                Debug.Log("move1");

            }
            else if (midy - Mathf.Abs(zScalar) * Mathf.Abs(z) < bottomPortal.position.y)
            {

                midy = bottomPortal.position.y + (Mathf.Abs(zScalar) * Mathf.Abs(z));
                Debug.Log("move2");

            }
        }
        else
        {
            yrange = resolution.width * yrange / resolution.height;
            z = yrange;
            if (midx + Mathf.Abs(zScalar) * Mathf.Abs(z) * (resolution.width / resolution.height) > rightPortal.position.x) 

            {
                midx = rightPortal.position.x - (Mathf.Abs(zScalar) * Mathf.Abs(z) * (resolution.width / resolution.height));
                Debug.Log("move3");

            }
            else if (midx - Mathf.Abs(zScalar) * Mathf.Abs(z) * (resolution.width / resolution.height) < leftPortal.position.x)
            {

                midx = leftPortal.position.x + (Mathf.Abs(zScalar) * Mathf.Abs(z) * (resolution.width / resolution.height));
                Debug.Log("move4");

            }
        }
        
        holder.position = Vector3.Lerp(holder.position, new Vector3(midx, midy, zScalar * z), smoothing * Time.deltaTime);
        cam.orthographicSize = holder.position.z;
    }

    void OnEnable()
    {
        originalPos = Camera.main.transform.localPosition;
    }

    void Update()
    {
        //if shakestart then set original and start shaking
        if (shake > 0 && !shakeStart)
        {
            shakeStart = true;
        }
        //if shake has started and still shake left
        else if (shake > 0 && shakeStart)
        {
            Camera.main.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else if (shake <= 0 && shakeStart)
        {
            shake = 0f;
            Camera.main.transform.localPosition = originalPos;
        }
        else
        {
            Camera.main.transform.localPosition = originalPos;
        }
    }

    //COME ON AND SLAM
    public void screenSlam(float shakeAmount, float shake)
    {
        this.shake = shake;
        this.shakeAmount = shakeAmount;
    }
}

