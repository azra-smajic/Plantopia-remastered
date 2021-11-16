using planTopia.Controllers.Player;
using planTopia.Core;
using planTopia.ScriptabileObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class OrdinaryCollideHealthDecrease : BaseBehaviour
    {
        [SerializeField]
        private ShootingAttributes shootingAttributes;

        [SerializeField]
        private ParticleSystem particle;
        [SerializeField]
        private AudioSource AudioSource;

        private void Start()
        {
            AudioSource = this.GetComponent<AudioSource>();
            AudioSource.Stop();
            particle.Stop();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tag.PLAYER)
            {
                AudioSource.Play();
                particle.Play();
                other.gameObject.GetComponent<PlayerHealth>().DecreaseHealth(shootingAttributes.Damage);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == Constants.Tag.PLAYER)
            {
             
                particle.Stop();
                
            }
        }
    }
}
