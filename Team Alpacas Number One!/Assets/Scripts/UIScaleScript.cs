using UnityEngine;
using System.Collections;

public class UIScaleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setUI(int numPlayers){
		float i=1f;
		print (transform);
		foreach(Transform child in transform){
			print ("DERP");
			float width=this.GetComponent<RectTransform>().rect.width;
			print (width);
			child.localScale=new Vector2(0.5f,0.5f);
			child.localPosition= new Vector2((width*(i/numPlayers))-(width/(i*2)),0);
			i+=1;
		}
	}
}
