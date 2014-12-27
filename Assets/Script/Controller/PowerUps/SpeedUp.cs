using UnityEngine;
using System.Collections;

namespace Drop{
	public class SpeedUp : MonoBehaviour {
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerEnter2D(Collider2D collider){
			collider.gameObject.GetComponent<PlayerController> ().SpeedUp ();
			GameObject.Destroy (gameObject);
		}
	}
}

