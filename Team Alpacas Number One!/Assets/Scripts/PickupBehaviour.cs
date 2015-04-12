using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PickupBehaviour : MonoBehaviour {

    public GameObject loopUIElement;
    private PowerUp powerUp;
    private AudioSource sound;
    Dictionary<int, loopObject> currentPositions; //int is player ID, float is their relative position
    public AudioClip loopEnter;
    public AudioClip loopComplete;
    public AudioClip loopExit;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
		Debug.Log(gameObject.name);

        currentPositions = new Dictionary<int, loopObject>();
        powerUp = GetComponent<PowerUp>();

        powerUp.PrintPowerUp();
    }

    public void Trigger() //all pickups should override this method
    {
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        sound.clip = loopEnter;
        sound.Play();
        if (!other.tag.Contains("Player"))
            return;
        GameObject UI;
        Vector2 translation = other.transform.position - this.transform.position;
        UI = Instantiate(loopUIElement, this.transform.position, Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(translation.y, translation.x)))) as GameObject;
        currentPositions[other.gameObject.GetInstanceID()] = new loopObject(translation, UI);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.tag.Contains("Player"))
            return;
        currentPositions[other.gameObject.GetInstanceID()].updatePosition(other.transform.position - this.transform.position);

        if (currentPositions[other.gameObject.GetInstanceID()].loopComplete())
        {
            sound.clip = loopComplete;
            sound.Play();
            currentPositions[other.gameObject.GetInstanceID()].destroy();
            currentPositions.Remove(other.gameObject.GetInstanceID());
			if(gameObject.name == "Pufferfish"){
				other.gameObject.GetComponent<PlayerState> ().changeActivePuffer ();
				powerUp.Activate(other.gameObject);
			}
			else{
				other.SendMessage("SetItem", powerUp);
				Trigger();
			}
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        sound.clip = loopExit;
        sound.Play();
        if (!other.tag.Contains("Player"))
            return;
        currentPositions[other.gameObject.GetInstanceID()].destroy();
        currentPositions.Remove(other.gameObject.GetInstanceID());
    }

    public void EraseAllUI(GameObject p1, GameObject p2)
    {
        if (currentPositions.ContainsKey(p1.GetInstanceID()) && currentPositions.ContainsKey(p2.GetInstanceID()))
        {
            currentPositions[p1.GetInstanceID()].destroy();
            currentPositions[p2.GetInstanceID()].destroy();
            currentPositions.Remove(p1.GetInstanceID());
            currentPositions.Remove(p2.GetInstanceID());
        }
    }

    public class loopObject
    {
        public Vector2 currentPosition;
        public float totalAngleCovered;
        public Vector2 startPosition;
        private GameObject UIArc;
        private Image ArcImage;
        public loopObject(Vector2 position, GameObject UIArc)
        {
            startPosition = position;
            currentPosition = position;
            totalAngleCovered = 0;
            this.UIArc = UIArc;
            ArcImage = UIArc.GetComponent<Image>();
        }

        public void updatePosition(Vector2 position)
        {
            totalAngleCovered += getAngleBetweenVectors(position, currentPosition);
            currentPosition = position;
            ArcImage.fillAmount = Mathf.Abs(totalAngleCovered) / 360; //fillAmount is [0,1]
            ArcImage.fillClockwise = totalAngleCovered >= 0;
        }

        public bool loopComplete()
        {
            return Mathf.Abs(totalAngleCovered) >= 360;
        }

        public void destroy()
        {
            Destroy(UIArc);
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

            return (Mathf.Atan2(vectorNew.x, vectorNew.y) - Mathf.Atan2(vectorOld.x, vectorOld.y)) * Mathf.Rad2Deg; //wait, what? it works, but the atan2 parameters are switched
        }
    }
}

