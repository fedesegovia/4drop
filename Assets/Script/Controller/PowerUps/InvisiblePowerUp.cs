using UnityEngine;
using System.Collections;
namespace Drop{
	public class InvisiblePowerUp : MonoBehaviour {
		void OnTriggerEnter2D(Collider2D collider){
			PlayerController player = collider.gameObject.GetComponent<PlayerController> ();
			player.GhostMode ();
			GameObject.Destroy (gameObject);
		}
	}
}