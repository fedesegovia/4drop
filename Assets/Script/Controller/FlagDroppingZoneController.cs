using UnityEngine;
using System.Collections;

namespace Drop{
    public class FlagDroppingZoneController : MonoBehaviour {

        public GameObject zoneCollider;
        bool flagIsInZone = false;

	    // Use this for initialization
	    void Start () {
	
	    }
	
	    // Update is called once per frame
	    void Update () {
	
	    }

	    void OnTriggerEnter2D(Collider2D collider){
            if (CollisionIsPlayer(collider))
            {
                PlayerController player = GetPlayerController(collider.gameObject);
                if (player.HasFlag())
                    OpenFlagDroppingZone();
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (CollisionIsPlayer(collider))
            {
                PlayerController player = GetPlayerController(collider.gameObject);
                if (player.HasFlag())
                    CloseFlagDroppingZone();
            }
        }

        bool CollisionIsPlayer(Collider2D collider)
        {
            PlayerController player = GetPlayerController(collider.gameObject);
            if (player != null)
                return true;
            return false;
        }


        PlayerController GetPlayerController(GameObject player)
        {
            return player.GetComponent<PlayerController>();
        }

        void OpenFlagDroppingZone()
        {
            zoneCollider.collider2D.enabled = false;
        }

        void CloseFlagDroppingZone()
        {
            zoneCollider.collider2D.enabled = true;
        }
    }

}
