using UnityEngine;
using System.Collections;
namespace Drop
{
	public class FlagPickUpController : MonoBehaviour {

		public GameObject flag;
		bool isFlagInBase = true;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnTriggerEnter2D(Collider2D collider){
			if (isFlagInBase && CollisionIsPlayer(collider)) {
				PlayerController player = GetPlayerController(collider.gameObject);
				player.AttachFlag(flag);
				isFlagInBase = false;
			}
		}

		bool CollisionIsPlayer(Collider2D collider){
			PlayerController player = GetPlayerController (collider.gameObject);
			if (player != null)
				return true;
			return false;
		}

		PlayerController GetPlayerController(GameObject player){
			return player.GetComponent<PlayerController> ();
		}
	}
}
