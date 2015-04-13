using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OffscreenPointer : MonoBehaviour {
    private GameObject cam;
    private RectTransform trans;
    private Vector2? target;
    public float duration;
    private float remainingDuration;
    private Image pointer;
    public float buffer = 50f;
	// Use this for initialization
	void Start () {
        cam = GameObject.FindWithTag("MainCamera");
        trans = GetComponent<RectTransform>();
        pointer = GetComponent<Image>();
        pointer.color = new Color(0,0,0,0);
	}
	
	// Update is called once per frame
    public void setTargetItem(Vector2 target)
    {
        this.target = target;
        remainingDuration = duration;
        pointer.color = new Color(1, 1, 1, 1);
    }

	void Update () {
        if (target != null)
        {
            Vector2 direction = (((Vector2)target) - (Vector2)(cam.transform.position));
            if (direction.magnitude < Mathf.Abs(cam.GetComponent<Camera>().orthographicSize))
            {
                trans.localPosition = direction * ((Screen.height / 2) / Mathf.Abs(cam.GetComponent<Camera>().orthographicSize));
            }
            else
            {
                trans.localPosition = ((Screen.height / 2) - buffer) * direction.normalized;
            }
            float targetDirection = (Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)) - 90;
            trans.rotation = Quaternion.Euler(0, 0, targetDirection);
        }

        if (remainingDuration < 0)
        {
            pointer.color = new Color(0,0,0,0);
            remainingDuration = 0f;
            target = null;
        }
        if (remainingDuration != 0)
        {
            remainingDuration -= Time.deltaTime;
        }
	}
}
