using UnityEngine;
using System.Collections;
namespace Drop{
	public class EscapeDoor : MonoBehaviour {

		bool isDoorOpen = true;
		BoxCollider2D collider;

		// Use this for initialization
		void Start () {
			collider = GetComponent<BoxCollider2D>();
			collider.enabled = false;
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerExit2D(){
			if (isDoorOpen) {
				collider.enabled = true;
				isDoorOpen = false;
				StartCoroutine(OpenDoor());
			}
				
		}

		IEnumerator OpenDoor(){
			yield return new WaitForSeconds (2);
			collider.enabled = false;
			isDoorOpen = true;
		}
	}
}

