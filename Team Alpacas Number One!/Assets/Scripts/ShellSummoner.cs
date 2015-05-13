using UnityEngine;
using System.Collections;

public class ShellSummoner : PowerUp
{
    public float speed;
    public float duration;
    public GameObject shellBulletPrefab;


    public void Start()
    {
        GameObject shell;
        shell = Instantiate(shellBulletPrefab, this.transform.position, this.transform.rotation) as GameObject;
        Destroy(shell, duration);
        shell.GetComponent<Rigidbody>().velocity = speed * Random.insideUnitCircle;
        Destroy(this.gameObject);
    }
}
