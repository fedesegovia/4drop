using UnityEngine;
using System.Collections;
namespace Drop{
	public class EscapeDoor : MonoBehaviour {
		bool isDoorOpen = true;
		BoxCollider2D collider;
        Animator animator;

		void Start () {
            animator = GetComponent<Animator>();
			collider = GetComponent<BoxCollider2D>();
			collider.enabled = false;
		}

		void OnTriggerExit2D(Collider2D colliderHit){
			if (isDoorOpen) {
				PlayerController player = colliderHit.GetComponent<PlayerController>();

				if((player && !player.HasFlag()) || (!player && colliderHit.tag == "Flag")){
	                CloseDoor();
					StartCoroutine(OpenDoor());
				}
			}				
		}

		IEnumerator OpenDoor(){
			yield return new WaitForSeconds (2);
            animator.SetBool("open", true);
			collider.enabled = false;
			isDoorOpen = true;

		}

        void CloseDoor(){
            animator.SetBool("open", false);
            collider.enabled = true;
            isDoorOpen = false;
        }
	}
}