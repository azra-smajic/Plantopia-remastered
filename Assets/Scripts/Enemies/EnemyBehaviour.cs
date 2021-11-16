using planTopia.Core;
using UnityEngine;

namespace planTopia.Enemies
{
    public class EnemyBehaviour : BaseBehaviour
    {
        [SerializeField]
        private Animator Animator;

        public Damage Damage;
        private EnemyController EnemieController { get; set; }
        private EnemyShooting enemyShooting { get; set; }
        



        private void Start()
        {
            EnemieController = this.GetComponent<EnemyController>();
            Animator = this.GetComponent<Animator>();
            enemyShooting = this.GetComponent<EnemyShooting>();
            Damage.StartingDamage = Damage.AmountOfDamage;

        }

        private void FixedUpdate()
        {
            if (startMove)
            {

                EnemieController.MoveAndRotate();
                if (enemyShooting != null)
                {
                    enemyShooting.Player = EnemieController.Player;
                    enemyShooting.OnStartFiring();
                }

            }



        }
    
        private void OnEnable()
        {
            Animator.SetBool("Run", true);
            startMove = true;

        }
        private void OnDisable()
        {

            if (this.gameObject.activeInHierarchy)
            {
                startMove = false;

                Animator.SetBool("Run", false);



            }
        }
    }
}
