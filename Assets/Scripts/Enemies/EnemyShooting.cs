using planTopia.Controllers.Player;
using planTopia.ScriptabileObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Enemies
{
    public class EnemyShooting : MonoBehaviour
    {


        [SerializeField]
        private ShootingAttributes shootingAttributes;

        [SerializeField]
        private ParticleSystem ParticleSystemStart;
       

        private float NextRate { get; set; } = 2;
        public Transform Player;
        [SerializeField]
        private AudioSource Audio;


        private Ray ray;
        private RaycastHit hitInfo;

        private void Start()
        {
            NextRate = Time.time;
        }



        public void OnStartFiring(float FireRate)
       {
            if (Time.time > NextRate && !Player.GetComponent<PlayerHealth>().isDeath)
            {
                NextRate = Time.time + shootingAttributes.FireRate;
                Transform ShootingPoint = this.transform;
                var direction= Player.position - ShootingPoint.position;
                if (direction.magnitude < shootingAttributes.ShootingDistance)
                {
                    NextRate = Time.time + FireRate;
                    ray.origin = gameObject.transform.position;
                    ray.direction = direction;

                    ParticleSystemStart.Play();
                    Audio.Play();
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider.gameObject.tag == "Player")
                            hitInfo.collider.gameObject.GetComponent<PlayerHealth>()?.DecreaseHealth(shootingAttributes.Damage);
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 4f);
                    }
                }
            }
            if (Player.GetComponent<PlayerHealth>().isDeath)
                ParticleSystemStart.Stop();


        }
    }
}
