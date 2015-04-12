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
    public float invincibility = 10.0f;
    public GameObject respawnPoint;
    public GameObject deathPoint;
    Collider collid;
    SpriteRenderer spriRender;
    Vector3 respawnPos;
    Vector3 deathPos;
    bool isDead = false;

    // Use this for initialization
	void Start () {
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

    public void loseLife()
    {
        numLives -= 1;
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(respawn());
        }
        collid.enabled = false;
        //spriRender.enabled = false;
        shake.screenSlam(0.2f,0.5f);
        trail.clearTrail();
        if (numLives < 0)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
        GameObject deadPlane;
        deadPlane = Instantiate(deadPlanePrefab, this.transform.position, this.transform.rotation) as GameObject;
        Destroy(deadPlane, 5);
        deadPlane.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
        //this.transform.position = new Vector3(0, 0, 0);
		Destroy (hearts [numLives - 1]);
    }

    IEnumerator respawn()
    {
        this.transform.position = deathPos;
        yield return new WaitForSeconds(5.0f);
        //spriRender.enabled = true;
        this.transform.position = respawnPos;
    }
}
