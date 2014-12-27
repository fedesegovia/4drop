using UnityEngine;
using System.Collections;

namespace Drop{
	public class VulnerableDoor : MonoBehaviour {

		public float secondsToDestroy = 1;
        public float timeOpen = 5;
        float timer;
		BoxCollider2D collider;
		MeshRenderer renderer;

		// Use this for initialization
		void Start () {
            timer = secondsToDestroy;
            collider = GetComponent<BoxCollider2D> ();
			renderer = GetComponent<MeshRenderer> ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnCollisionStay2D(){
			timer -= Time.deltaTime;
			Color materialColor = renderer.material.color;
			materialColor.a -= Time.deltaTime;
			renderer.material.color = materialColor;
			if (timer < 0) {
				collider.enabled = false;
                StartCoroutine(CloseDoor());
			}
		}

        IEnumerator CloseDoor()
        {
            yield return new WaitForSeconds(timeOpen);
            Color materialColor = renderer.material.color;
            materialColor.a = 1;
            renderer.material.color = materialColor;
            collider.enabled = true;
            timer = secondsToDestroy;
        }
	}
}
