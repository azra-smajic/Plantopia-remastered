using planTopia.Controllers.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class ActivateWall : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem wall;
        void Start()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.PLAYER)
            {
                wall.Play();
                other.gameObject.GetComponent<PlayerHealth>().CurrentHealth = other.gameObject.GetComponent<PlayerHealth>().CurrentHealth -5;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            wall.Stop();
        }
    }
}
