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

    // Use this for initialization
	void Start () {
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

    }

    public void loseLife()
    {
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
}
