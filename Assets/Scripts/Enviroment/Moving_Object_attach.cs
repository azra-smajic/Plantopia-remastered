using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Enviroment
{
    public class Moving_Object_attach : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.PLAYER))
            {
                player.transform.parent = this.transform;
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.PLAYER))
            {
                player.transform.parent = null;
            }
        }


    }
}
