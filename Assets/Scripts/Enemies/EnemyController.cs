using planTopia.Controllers.Player;
using planTopia.Enviroment;
using UnityEngine;

namespace planTopia.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Speed speed;
        [SerializeField]
        private ParticleSystem particleNight;
        [SerializeField]
        private ParticleSystem particleDay;
  
        public Transform Player { get; set; }
        private Rigidbody Rigidbody { get; set; }
       
        private DayAndNightController dayAndNightController { get; set; }
        bool isDayEnemy;
        public bool isDayParicle=true;




        private void Start()
        {
            Rigidbody = this.GetComponent<Rigidbody>();
            dayAndNightController = GameObject.FindWithTag("DayNight").GetComponent<DayAndNightController>();
          
            dayAndNightController.OnTimeOfDayChanged += ParticlesStart;
        }
        private void ParticlesStart(bool isDay)
        {
            if (isDayParicle != isDay)
            {
                if (isDay) 
                    particleDay.Play();
                //else
                    //particleNight.Play();
                    isDayParicle = isDay;
            }

        }
        
        public void MoveAndRotate()
        {
            if (Player.GetComponent<PlayerHealth>().isDeath)
                return;
            Vector3 direction = Player.position - this.transform.position;
            if (Vector3.Distance(this.transform.position, Player.position) > 0.7)
            {
                direction.Normalize();
                Vector3 movement = direction;
                Rigidbody.MovePosition(this.transform.position + (movement * Time.deltaTime * speed.speed));
            }
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Rigidbody.MoveRotation(Quaternion.Euler(0f, angle, 0f));

            
           

        }
        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.CompareTag(Constants.Tag.PLAYER))
        //    {
        //        Player.GetComponent<EnemyHealthRegulator>()?.DecreaseHealth(this.GetComponent<Enabled>().Damage.AmountOfDamage);
        //    }
        //}
       
    }
}
