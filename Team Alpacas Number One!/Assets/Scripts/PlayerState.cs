﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private bool activePuffer = false;

    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        spriRender = GetComponent<SpriteRenderer>();
        collid = GetComponent<Collider>();
        deathPos = deathPoint.transform.position;
        respawnPos = respawnPoint.transform.position;
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>();
        trail = GetComponent<LineCollision>();
		GameObject playerFrame = GameObject.Find ("CanvasPrefab/PlayerFrame");
		ui = Instantiate (playerUI) as GameObject;
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

	public void changeActivePuffer(){
		if (activePuffer) {
			activePuffer = false;
		} else {
			activePuffer = true;
		}
	}

	public bool getActivePuffer(){
		return activePuffer;
	}
	
	public void loseLife()
    {
        if (invulnerable)
            return;
        GameObject deadPlane;
        deadPlane = Instantiate(deadPlanePrefab, this.transform.position, this.transform.rotation) as GameObject;
        Destroy(deadPlane, 5);
        deadPlane.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
        if (!isDead)
        {
            isDead = true;
            gm.clearAllItemUI();
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

		Destroy (hearts [numLives - 1]);
    }

    IEnumerator respawn()
    {
        //print("fucking");
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

	public void setUIItem(PowerUp powerUp){
		print (ui.transform);
		Image UIItem = ui.transform.FindChild ("ItemImage").GetComponent<Image> ();
		UIItem.sprite = powerUp.gameObject.GetComponent<SpriteRenderer> ().sprite;
		UIItem.color = Color.white;
	}

	public void removeUIItem(){
		Image UIItem = ui.transform.FindChild ("ItemImage").GetComponent<Image> ();
		UIItem.sprite = null;
		Color color = UIItem.color;
		color.a = 0;
		UIItem.color = color;
	}
}
