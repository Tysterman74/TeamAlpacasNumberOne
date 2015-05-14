using UnityEngine;
using System.Collections;

public class AITrailSummoner : PowerUp
{
    public float speed;
    public float duration;
    public GameObject AITrailPrefab;


    public void Start()
    {
        GameObject trail;
        trail = Instantiate(AITrailPrefab, this.transform.position, this.transform.rotation) as GameObject;
        Destroy(trail, duration);
        Vector2 velocity = speed * Random.insideUnitCircle.normalized;
        trail.GetComponent<Rigidbody>().velocity = velocity;
        trail.transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(((Vector2)velocity).y, ((Vector2)velocity).x) * Mathf.Rad2Deg) - 90, Vector3.forward);
        GameObject.Find("GameManager").GetComponent<GameManager>().RemoveItem(this.gameObject);
        Destroy(this.gameObject);
    }
}
