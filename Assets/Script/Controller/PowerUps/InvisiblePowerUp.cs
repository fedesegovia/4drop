using UnityEngine;
using System.Collections;
namespace Drop{
	public class InvisiblePowerUp : MonoBehaviour {
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		void OnTriggerEnter2D(Collider2D collider){
			PlayerController player = collider.gameObject.GetComponent<PlayerController> ();
			player.GhostMode ();
			GameObject.Destroy (gameObject);
		}
	}
}

