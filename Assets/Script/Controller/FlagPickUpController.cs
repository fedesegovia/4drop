using UnityEngine;
using System.Collections;
namespace Drop
{
	public class FlagPickUpController : MonoBehaviour {
		public Collider2D pickUpTrigger = null;

		void OnTriggerEnter2D(Collider2D collider){
			PlayerController player = GetPlayerController (collider.gameObject);

			if (player != null && !player.HasFlag()) {
				player.AttachFlag(this.gameObject);
			}
		}

		PlayerController GetPlayerController(GameObject player){
			return player.GetComponent<PlayerController> ();
		}

		public void EnablePickingUp(){
			if(pickUpTrigger)
				pickUpTrigger.enabled = true;
		}

		public void DisablePickingUp(){
			if(pickUpTrigger)
				pickUpTrigger.enabled = false;
		}
	}
}
