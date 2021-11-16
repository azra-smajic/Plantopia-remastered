using UnityEngine;

namespace planTopia.Core
{
    public class DizzyActivator :MonoBehaviour
    {
        [SerializeField]
        private Animator Animator;
        [SerializeField]
        private Damage Damage;


       

        private void Start()
        {

                Damage.StartingDamage = Damage.AmountOfDamage;
                Animator = this.GetComponent<Animator>();
              
            

        }
        public void StartDizzy()
        {
            Animator.SetBool("Dizzy",true);


            Damage.AmountOfDamage = 0;
            Invoke("StopDizzy", 2);
            
        }
        private void StopDizzy()
        {

            Animator.SetBool("Dizzy", false);

            Damage.AmountOfDamage = 1;
            this.GetComponent<BaseBehaviour>().startMove = true;
          
            
        }
    }
}
