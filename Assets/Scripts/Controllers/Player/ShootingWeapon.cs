using planTopia.Controllers.Core;
using planTopia.Core;
using planTopia.Enemies;
using planTopia.ScriptabileObjects;
using UnityEngine;
using UnityEngine.UI;

namespace planTopia.Controllers.Player
{
    public class ShootingWeapon : MonoBehaviour
    {
        [SerializeField]
        private SFX EmptyGunSound;
        [SerializeField]
        private InputManagment InputManagment;
        [SerializeField]
        private Transform RaycastOrigin;
        [SerializeField]
        private Transform RaycastDestination;
        [SerializeField]
        private Text UIAmmunationText;
        [SerializeField]
        private ParticleSystem ParticleShoot;
        private ShootingAttributes shootingAttributes { get; set; }
        private SFX CurrentGunSound { get; set; }
        private GameObject Object { get; set; }
        private AudioManager AudioManager { get; set; }

        private float NextRate;
        private Ray ray;
        private RaycastHit hitInfo;

        private void Start()
        {
            InputManagment.OnShooting += OnStartFiring;
            //AudioManager = this.GetComponent<AudioManager>();
        }

        private void OnStartFiring()
        {

            if (Time.time > NextRate)
            {
                if (shootingAttributes.CurrentAmmunation > 0)
                {
                    NextRate = Time.time + shootingAttributes.FireRate;
                    ray.origin = RaycastOrigin.position;
                    ray.direction = RaycastDestination.position - RaycastOrigin.position;
                    ParticleShoot.Play();
                    //AudioManager.Play(CurrentGunSound);
                    DecreaseAmmunation();

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider.gameObject.tag == Constants.Tag.Enemy)
                        {
                            Object = hitInfo.collider.gameObject;
                            var BaseGenericsEnemy = Object.GetComponent<BaseBehaviour>();
                            if (Vector3.Distance(Object.transform.position, this.transform.position) < shootingAttributes.ShootingDistance)
                            {
                                // hitInfo.collider.gameObject.GetComponent<EnemyHealthRegulator>()?.DecreaseHealth(shootingAttributes.Damage);
                                // BaseGenericsEnemy.startMove = false;
                                // Object.GetComponent<DizzyActivator>().StartDizzy();
                                
                                Object.GetComponent<EnemyAI>().TakeDamage(shootingAttributes.Damage);
                            }
                        }
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 4f);
                    }
                }
//                else AudioManager.Play(EmptyGunSound);
            }

        }
        private void DecreaseAmmunation()
        {
            shootingAttributes.CurrentAmmunation -= 1;
            UIAmmunationText.text = $"{shootingAttributes.CurrentAmmunation.ToString()}/{shootingAttributes.MaxAmmunation.ToString()}";
        }
        public void SetShootingAttributes(ShootingAttributes Gun)
        {
            shootingAttributes = Gun;
            CurrentGunSound = Gun.GunSound;
        }
    }
}
