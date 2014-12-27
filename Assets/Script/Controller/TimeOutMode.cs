using UnityEngine;
using System.Collections;

public class TimeOutMode : MonoBehaviour {

	public float timeOutSeconds = 15;

	// Use this for initialization
	void Start () {
		StartCoroutine (TimeOut ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator TimeOut(){
		yield return new WaitForSeconds (timeOutSeconds);
		Application.LoadLevel (Application.loadedLevel);
	}
}
