using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RainbowEffect : MonoBehaviour {
    private float colorIncrement;
    private ParticleSystem par;
	// Use this for initialization
	void Start () {
        par = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        colorIncrement += 150*Time.deltaTime;
        float red = ((2 * Mathf.Abs(((colorIncrement + 0) % 255) - 127.5f)));
        if (red < 0)
            red = 0;
        if (red > 255)
            red = 255;
        float blue = ((2 * Mathf.Abs(((colorIncrement -85) % 255) - 127.5f)));
        if (blue < 0)
            blue = 0;
        if (blue > 255)
            blue = 255;
        float green = ((2 * Mathf.Abs(((colorIncrement + 85) % 255) - 127.5f)));
        if (green < 0)
            green = 0;
        if (green > 255)
            green = 255;
        par.startColor = new Color(red / 255, blue / 255, green / 255, 0.5f);
	}
}
