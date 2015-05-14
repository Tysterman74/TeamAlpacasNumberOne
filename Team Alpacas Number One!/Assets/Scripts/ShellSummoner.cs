using UnityEngine;
using System.Collections;

public class ShellSummoner : PowerUp
{
    public float speed;
    public float duration;
    public GameObject shellBulletPrefab;


    public override void Start()
    {
        GameObject shell;
        shell = Instantiate(shellBulletPrefab, this.transform.position, this.transform.rotation) as GameObject;
        Destroy(shell, duration);
        shell.GetComponent<Rigidbody>().velocity = speed * Random.insideUnitCircle.normalized;
        GameObject.Find("GameManager").GetComponent<GameManager>().RemoveItem(this.gameObject);
        Destroy(this.gameObject);
    }
}
