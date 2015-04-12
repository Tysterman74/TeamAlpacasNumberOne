using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	public void LoadScene (string sceneName) {
		Application.LoadLevel (sceneName);
	}

	public void exitGame (){
		Application.Quit ();
	}
}
