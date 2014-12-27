using UnityEngine;
using System.Collections;
namespace Drop{
	public class EscapeDoor : MonoBehaviour {

		bool isDoorOpen = true;
		BoxCollider2D collider;
        Animator animator;

		// Use this for initialization
		void Start () {
            animator = GetComponent<Animator>();
			collider = GetComponent<BoxCollider2D>();
			collider.enabled = false;
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerExit2D(){
			if (isDoorOpen) {
                CloseDoor();
				StartCoroutine(OpenDoor());
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

