using UnityEngine;
using System.Collections;

namespace Drop
{ 
    public class BulletController : MonoBehaviour {
		public Weapon parentWeapon = null;
		public Vector3 direction;
        public float speed = 60f;
		public int damage = 3;
		public bool inWeapon = true;

	    void Update () {
			transform.position += direction.normalized * speed * Time.deltaTime;
	    }

		void OnBecameInvisible(){
			RecycleBullet();
		}

		void OnTriggerEnter2D(Collider2D hitCollider){
			if(hitCollider.gameObject != parentWeapon.transform.parent.gameObject){
				PlayerController player = hitCollider.GetComponent<PlayerController>();

				if(player){
					player.AbsorbDamage(damage);
					RecycleBullet();
				}
			}
		}

		void RecycleBullet(){
			if(parentWeapon && !inWeapon){
				parentWeapon.LoadBullet(this);
			}
		}
    }
}