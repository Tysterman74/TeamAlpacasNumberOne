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
		float i=0f;
		foreach(Transform child in transform){
			float width=this.GetComponent<RectTransform>().rect.width;
			print ("Width: " + width);
			child.localScale=new Vector2(0.5f,0.5f);
			child.localPosition= new Vector2(((width*(i/numPlayers))+(width*((i+1)/numPlayers)))/2-(width/2),0);
			i+=1;
		}
	}
}
