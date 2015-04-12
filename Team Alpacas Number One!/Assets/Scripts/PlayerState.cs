using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
    public int numLives;
    private CameraShakeScript shake;
    private LineCollision trail;
    public GameObject deadPlanePrefab;
	public GameObject playerUI;
	public GameObject Heart;
	private GameObject ui;
	private GameObject heartContainer;
	private GameObject[] hearts;
    private bool invulnerable = false;
    public float invincibility = 10.0f;
    public GameObject respawnPoint;
    public GameObject deathPoint;
    public float spawnDistance;
    Collider collid;
    SpriteRenderer spriRender;
    Vector3 respawnPos;
    Vector3 deathPos;
    bool isDead = false;
    private float width;
    private float height;
    public float border;

    // Use this for initialization
    void Start()
    {
        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        spriRender = GetComponent<SpriteRenderer>();
        collid = GetComponent<Collider>();
        deathPos = deathPoint.transform.position;
        respawnPos = respawnPoint.transform.position;
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>();
        trail = GetComponent<LineCollision>();
		GameObject playerFrame = GameObject.Find ("Canvas/PlayerFrame");
		GameObject ui = Instantiate (playerUI) as GameObject;
		ui.transform.parent=playerFrame.transform;
		heartContainer = ui.transform.FindChild ("HeartContainer").gameObject;
		hearts = new GameObject[numLives];
		for (int i=0; i<numLives; i++) {
			GameObject heart = Instantiate (Heart) as GameObject;
			hearts[i]=heart;
			heart.transform.parent=heartContainer.transform;
		}
	}
	
	// Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (invincibility <= 0.0f)
            {
                isDead = false;
                invincibility = 10.0f;
                collid.enabled = true;
            }
            invincibility -= Time.deltaTime;
        }
    }

	public void setInvulnerability(bool invulnerable)
	{
		this.invulnerable = invulnerable;
	}

    public void loseLife()
    {
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(respawn());
        }
        collid.enabled = false;
        numLives -= 1;
        shake.screenSlam(0.2f,0.5f);
        trail.clearTrail();
        if (numLives < 0)
        {
            Debug.Log("Dead");
        }
        GameObject deadPlane;
        deadPlane = Instantiate(deadPlanePrefab, this.transform.position, this.transform.rotation) as GameObject;
        Destroy(deadPlane, 5);
        deadPlane.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
        this.transform.position = new Vector3(0, 0, 0);
		Destroy (hearts [numLives - 1]);
    }

    IEnumerator respawn()
    {
        this.transform.position = deathPos;
        yield return new WaitForSeconds(5.0f);
        Vector3 spawnPoint = new Vector3(Random.Range((border - width / 2), (width / 2 - border)), Random.Range((border - height / 2), (height / 2 - border)), 0.0f);
        Vector3 offSet = new Vector3(100.0f, 100.0f, 0.0f);
        /**/
        if (!(Physics.CheckSphere(spawnPoint, spawnDistance)))
        {
            this.transform.position = spawnPoint;
        }
        else
        {
            //spawn in an offset
            this.transform.position = spawnPoint + offSet;
        }
    }
}
