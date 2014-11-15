using UnityEngine;
using System.Collections;

namespace Drop
{ 
    public class BulletController : MonoBehaviour {

        public PlayerController Player;
        public Vector3 Direction;
        public float Speed = 60f;
        public float TimeToComeBack = 1;
        float counter = 0;

	    void FixedUpdate () {
            transform.position += Direction * Speed * Time.deltaTime;
	    }
    }
}